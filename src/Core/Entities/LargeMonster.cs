using System.Numerics;

using REFrameworkNET;

using ValueType = REFrameworkNET.ValueType;

namespace YURI_Overlay;

internal sealed class LargeMonster
{
	public ManagedObject EnemyCharacter;

	public string Name = "Large Monster";
	public Vector3 Position = Vector3.Zero;
	public float Distance = 0f;
	public float Health = -1;
	public float MaxHealth = -1;
	public float HealthPercentage = -1;
	public bool IsAlive = false;

	public LargeMonsterStaticUi StaticUi;

	private Type String_Type;
	private Type vec3_Type;
	private Type chealthManager_Type;
	private Type Single_Type;


	private Method NameString_Method;
	private Method get_Pos_Method;

	private Method get_HealthMgr_Method;
	private Method get_Health_Method;
	private Method get_MaxHealth_Method;

	private Field _Context_Field;
	private Field _Em_Field;
	private Field Basic_Field;
	private Field EmID_Field;
	private Field RoleID_Field;
	private Field LegendaryID_Field;
	private Field x_Field;
	private Field y_Field;
	private Field z_Field;

	public LargeMonster(ManagedObject enemyCharacter)
	{
		EnemyCharacter = enemyCharacter;

		try
		{
			InitializeTdb();
			Initialize();
			UpdateHealth();

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
			var pos = (ValueType) get_Pos_Method.InvokeBoxed(vec3_Type, EnemyCharacter, []);
			if(pos == null)
			{
				LogManager.Info("[LargeMonster.UpdatePosition] No enemy pos");
				return;
			}

			var x = (float?) x_Field.GetDataBoxed((ulong) pos.Ptr(), false);
			if(x == null)
			{
				LogManager.Info("[LargeMonster.UpdatePosition] No enemy pos x");
				return;
			}

			var y = (float?) y_Field.GetDataBoxed((ulong) pos.Ptr(), false);
			if(y == null)
			{
				LogManager.Info("[LargeMonster.UpdatePosition] No enemy pos y");
				return;
			}

			var z = (float?) z_Field.GetDataBoxed((ulong) pos.Ptr(), false);
			if(z == null)
			{
				LogManager.Info("[LargeMonster.UpdatePosition] No enemy pos z");
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
		Distance = 0f;
	}

	// Has to be called from in-game update method, because it is encrypted protected memory.
	public void UpdateHealth()
	{
		try
		{
			var healthManager = (ManagedObject) get_HealthMgr_Method.InvokeBoxed(chealthManager_Type, EnemyCharacter, []);
			if(healthManager == null)
			{
				LogManager.Info("[LargeMonster.UpdateHealth] No health manager");
				return;
			}

			var health = (float?) get_Health_Method.InvokeBoxed(Single_Type, healthManager, []);
			if(health == null)
			{
				LogManager.Info("[LargeMonster.UpdateHealth] No health");
				return;
			}

			var maxHealth = (float?) get_MaxHealth_Method.InvokeBoxed(Single_Type, healthManager, []);
			if(maxHealth == null || Utils.IsApproximatelyEqual((float) maxHealth, 0f))
			{
				LogManager.Info("[LargeMonster.UpdateHealth] No max health");
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

			var cEmModuleBasic_TypeDef = Basic_Field.GetType();

			EmID_Field = cEmModuleBasic_TypeDef.GetField("EmID");
			RoleID_Field = cEmModuleBasic_TypeDef.GetField("RoleID");
			LegendaryID_Field = cEmModuleBasic_TypeDef.GetField("LegendaryID");

			var EnemyDef_TypeDef = TDB.Get().GetType("app.EnemyDef");

			NameString_Method = EnemyDef_TypeDef.GetMethod("NameString");

			String_Type = NameString_Method.ReturnType.GetType();

			get_Pos_Method = EnemyCharacter_TypeDef.GetMethod("get_Pos");

			var vec3_TypeDef = get_Pos_Method.ReturnType;
			vec3_Type = vec3_TypeDef.GetType();

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
				LogManager.Info("[LargeMonster.Initialize] No enemy context holder");
				return;
			}

			var _Em = (ManagedObject) _Em_Field.GetDataBoxed((ulong) _Context.Ptr(), false);
			if(_Em == null)
			{
				LogManager.Info("[LargeMonster.Initialize] No enemy context");
				return;
			}

			var Basic = (ManagedObject) Basic_Field.GetDataBoxed((ulong) _Em.Ptr(), false);
			if(Basic == null)
			{
				LogManager.Info("[LargeMonster.Initialize] No enemy basic module");
				return;
			}

			var EmID = (int?) EmID_Field.GetDataBoxed((ulong) Basic.Ptr(), false);
			if(EmID == null)
			{
				LogManager.Info("[LargeMonster.Initialize] No enemy ID");
				return;
			}

			var RoleID = (int?) RoleID_Field.GetDataBoxed((ulong) Basic.Ptr(), false);
			if(RoleID == null)
			{
				LogManager.Info("[LargeMonster.Initialize] No enemy role ID");
				return;
			}

			var LegendaryID = (int?) LegendaryID_Field.GetDataBoxed((ulong) Basic.Ptr(), false);
			if(LegendaryID == null)
			{
				LogManager.Info("[LargeMonster.Initialize] No enemy legendary ID");
				return;
			}

			//var name = (string) NameString_Method.InvokeBoxed(String_Type, null, [EmID, RoleID, LegendaryID]);
			//if(name == null)
			//{
			//	LogManager.Info("[LargeMonster.Initialize] No enemy name");
			//	return;
			//}

			//Name = name;
			Name = "High Purrformance Barrel Puncher Silver #444";
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

}
