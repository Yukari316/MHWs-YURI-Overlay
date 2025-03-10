using System.Numerics;

using REFrameworkNET;

using ValueType = REFrameworkNET.ValueType;

namespace YURI_Overlay;

internal sealed class LargeMonster
{
	public ManagedObject EnemyCharacter;
	public ManagedObject _Em;

	public string Name = "Large Monster";
	public int Id = -1;
	public int RoleId = -1;
	public int LegendaryId = -1;


	public Vector3 MissionBeaconOffset = Vector3.Zero;
	public float ModelRadius = 0f;

	public Vector3 Position = Vector3.Zero;
	public float Distance = 0f;

	public float Health = -1;
	public float MaxHealth = -1;
	public float HealthPercentage = -1;
	public bool IsAlive = false;

	public LargeMonsterDynamicUi DynamicUi;
	public LargeMonsterStaticUi StaticUi;

	private Type String_Type;
	private Type chealthManager_Type;
	private Type Single_Type;

	private Method NameString_Method;

	private Method get_HealthMgr_Method;
	private Method get_Health_Method;
	private Method get_MaxHealth_Method;

	private Field _Context_Field;
	private Field _Em_Field;
	private Field Basic_Field;
	private Field EmID_Field;
	private Field RoleID_Field;
	private Field LegendaryID_Field;
	private Field ModelCenterPos_Field;
	private Field x_Field;
	private Field y_Field;
	private Field z_Field;
	private Field ModelRadius_Field;

	public LargeMonster(ManagedObject enemyCharacter)
	{
		EnemyCharacter = enemyCharacter;

		try
		{
			InitializeTdb();
			Initialize();
			UpdateHealth();

			DynamicUi = new LargeMonsterDynamicUi(this);
			StaticUi = new LargeMonsterStaticUi(this);

			LogManager.Info($"[LargeMonster] Initialized {Name}");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public unsafe void UpdatePosition()
	{
		try
		{
			// Can't cash for something reason :(
			var pos = (ValueType) EnemyCharacter.Call("get_Pos");
			//var pos = (ValueType) ModelCenterPos_Field.GetDataBoxed(vec3_Type, (ulong) _Em.Ptr(), true);
			if(pos == null)
			{
				LogManager.Warn("[LargeMonster.UpdatePositionAndDistance] No enemy pos");
				return;
			}

			var positionPointer = (ulong) pos.Ptr();

			var x = (float?) x_Field.GetDataBoxed(positionPointer, true);
			if(x == null)
			{
				LogManager.Warn("[LargeMonster.UpdatePositionAndDistance] No enemy pos x");
				return;
			}

			var y = (float?) y_Field.GetDataBoxed(positionPointer, true);
			if(y == null)
			{
				LogManager.Warn("[LargeMonster.UpdatePositionAndDistance] No enemy pos y");
				return;
			}

			var z = (float?) z_Field.GetDataBoxed(positionPointer, true);
			if(z == null)
			{
				LogManager.Warn("[LargeMonster.UpdatePositionAndDistance] No enemy pos z");
				return;
			}

			Position.X = (float) x;
			Position.Y = (float) y;
			Position.Z = (float) z;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void UpdateDistance()
	{
		Distance = ScreenManager.Instance.GetWorldPositionToCameraDistance(Position);
	}

	// Has to be called from in-game update method, because it is encrypted protected memory.
	public void UpdateHealth()
	{
		try
		{
			var healthManager = (ManagedObject) get_HealthMgr_Method.InvokeBoxed(chealthManager_Type, EnemyCharacter, []);
			if(healthManager == null)
			{
				LogManager.Warn("[LargeMonster.UpdateHealth] No health manager");
				return;
			}

			var health = (float?) get_Health_Method.InvokeBoxed(Single_Type, healthManager, []);
			if(health == null)
			{
				LogManager.Warn("[LargeMonster.UpdateHealth] No health");
				return;
			}

			var maxHealth = (float?) get_MaxHealth_Method.InvokeBoxed(Single_Type, healthManager, []);
			if(maxHealth == null || Utils.IsApproximatelyEqual((float) maxHealth, 0f))
			{
				LogManager.Warn("[LargeMonster.UpdateHealth] No max health");
				return;
			}

			Health = (float) health;
			MaxHealth = (float) maxHealth;
			HealthPercentage = (float) (health / maxHealth);

			IsAlive = !Utils.IsApproximatelyEqual((float) health, 0f);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void InitializeTdb()
	{
		try
		{
			var EnemyCharacter_TypeDef = TDB.Get().GetType("app.EnemyCharacter");

			get_HealthMgr_Method = EnemyCharacter_TypeDef.GetMethod("get_HealthMgr");
			_Context_Field = EnemyCharacter_TypeDef.GetField("_Context");

			_Em_Field = _Context_Field.GetType().GetField("_Em");

			var enemyContext_TypeDef = _Em_Field.GetType();

			Basic_Field = enemyContext_TypeDef.GetField("Basic");
			ModelRadius_Field = enemyContext_TypeDef.GetField("ModelRadius");

			var cEmModuleBasic_TypeDef = Basic_Field.GetType();

			EmID_Field = cEmModuleBasic_TypeDef.GetField("EmID");
			RoleID_Field = cEmModuleBasic_TypeDef.GetField("RoleID");
			LegendaryID_Field = cEmModuleBasic_TypeDef.GetField("LegendaryID");

			var EnemyDef_TypeDef = TDB.Get().GetType("app.EnemyDef");

			NameString_Method = EnemyDef_TypeDef.GetMethod("NameString");

			String_Type = NameString_Method.ReturnType.GetType();

			ModelCenterPos_Field = enemyContext_TypeDef.GetField("ModelCenterPos");

			var vec3_TypeDef = ModelCenterPos_Field.GetType();

			x_Field = vec3_TypeDef.GetField("x");
			y_Field = vec3_TypeDef.GetField("y");
			z_Field = vec3_TypeDef.GetField("z");

			var chealthManager_TypeDef = get_HealthMgr_Method.ReturnType;
			chealthManager_Type = chealthManager_TypeDef.GetType();

			get_Health_Method = chealthManager_TypeDef.GetMethod("get_Health");
			get_MaxHealth_Method = chealthManager_TypeDef.GetMethod("get_MaxHealth");

			Single_Type = get_Health_Method.ReturnType.GetType();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private unsafe void Initialize()
	{
		try
		{
			var _Context = (ManagedObject) _Context_Field.GetDataBoxed((ulong) EnemyCharacter.Ptr(), false);
			if(_Context == null)
			{
				LogManager.Warn("[LargeMonster.Initialize] No enemy context holder");
				return;
			}

			_Em = (ManagedObject) _Em_Field.GetDataBoxed((ulong) _Context.Ptr(), false);
			if(_Em == null)
			{
				LogManager.Warn("[LargeMonster.Initialize] No enemy context");
				return;
			}

			UpdateIds();
			UpdateName();
			UpdateMissionBeaconOffset();
			UpdateModelRadius();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private unsafe void UpdateIds()
	{
		try
		{
			var Basic = (ManagedObject) Basic_Field.GetDataBoxed((ulong) _Em.Ptr(), false);
			if(Basic == null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy basic module");
				return;
			}

			var basicPointer = (ulong) Basic.Ptr();

			// isValueType = false is intentional, otherwise, value is wrong
			var EmID = (int?) EmID_Field.GetDataBoxed(basicPointer, false);
			if(EmID == null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy Id");
				return;
			}

			// isValueType = false is intentional, otherwise, value is wrong
			var RoleID = (int?) RoleID_Field.GetDataBoxed(basicPointer, false);
			if(RoleID == null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy role Id");
				return;
			}

			// isValueType = false is intentional, otherwise, value is wrong
			var LegendaryID = (int?) LegendaryID_Field.GetDataBoxed(basicPointer, false);
			if(LegendaryID == null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy legendary Id");
				return;
			}

			Id = (int) EmID;
			RoleId = (int) RoleID;
			LegendaryId = (int) LegendaryID;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void UpdateName()
	{
		try
		{
			var name = (string) NameString_Method.InvokeBoxed(String_Type, null, [Id, RoleId, LegendaryId]);
			if(name == null)
			{
				LogManager.Warn("[LargeMonster.UpdateName] No enemy name");
				return;
			}

			Name = name;
			Name = "Tempered Guardian Fulgur Anjanath";
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
	private unsafe void UpdateMissionBeaconOffset()
	{
		try
		{
			// Can't cash for something reason :(
			var _MissionBeaconOffset = (ValueType) _Em.GetField("MissionBeaconOffset");
			//var pos = (ValueType) ModelCenterPos_Field.GetDataBoxed(vec3_Type, (ulong) _Em.Ptr(), true);
			if(_MissionBeaconOffset == null)
			{
				LogManager.Warn("[LargeMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset");
				return;
			}

			var missionBeaconOffsetPointer = (ulong) _MissionBeaconOffset.Ptr();

			var x = (float?) x_Field.GetDataBoxed(missionBeaconOffsetPointer, true);
			if(x == null)
			{
				LogManager.Warn("[LargeMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset x");
				return;
			}

			var y = (float?) y_Field.GetDataBoxed(missionBeaconOffsetPointer, true);
			if(y == null)
			{
				LogManager.Warn("[LargeMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset y");
				return;
			}

			var z = (float?) z_Field.GetDataBoxed(missionBeaconOffsetPointer, true);
			if(z == null)
			{
				LogManager.Warn("[LargeMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset z");
				return;
			}

			MissionBeaconOffset.X = (float) x;
			MissionBeaconOffset.Y = (float) y;
			MissionBeaconOffset.Z = (float) z;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private unsafe void UpdateModelRadius()
	{
		try
		{
			// isValueType = false is intentional, otherwise, value is wrong
			var _ModelRadius = (float?) ModelRadius_Field.GetDataBoxed(Single_Type, (ulong) _Em.Ptr(), false);
			if(_ModelRadius == null)
			{
				LogManager.Warn("[LargeMonster.UpdateModelRadius] No enemy model radius");
				return;
			}

			ModelRadius = (float) _ModelRadius;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}
