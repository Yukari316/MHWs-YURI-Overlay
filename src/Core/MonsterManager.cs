using REFrameworkNET;

namespace YURI_Overlay;

internal sealed class MonsterManager : IDisposable
{
	private static readonly Lazy<MonsterManager> Lazy = new(() => new MonsterManager());

	public static MonsterManager Instance => Lazy.Value;

	public Dictionary<ManagedObject, LargeMonster> LargeMonsters = [];

	private Type chealthManager_Type;
	private Type Boolean_Type;
	private Type String_Type;

	private Method doUpdateEnd_Method;
	private Method get_HealthMgr_Method;
	private Method get_IsBoss_Method;
	private Method NameString_Method;

	private Field _Context_Field;
	private Field _Em_Field;
	private Field Basic_Field;
	private Field EmID_Field;
	private Field RoleID_Field;
	private Field LegendaryID_Field;

	private MethodHook preDoUpdateEndHook;



	private MonsterManager() { }

	~MonsterManager()
	{
		Dispose();
	}

	public void Initialize()
	{
		try
		{
			LogManager.Info("MonsterManager: Initializing...");

			InitializeTdb();
			Hook();

			LogManager.Info("MonsterManager: Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info($"MonsterManager: Disposing...");

		preDoUpdateEndHook.Dispose();

		LogManager.Info($"MonsterManager: Disposed!");
	}

	private void InitializeTdb()
	{
		try
		{
			var EnemyCharacter_TypeDef = TDB.Get().GetType("app.EnemyCharacter");

			doUpdateEnd_Method = EnemyCharacter_TypeDef.GetMethod("doUpdateEnd");
			get_HealthMgr_Method = EnemyCharacter_TypeDef.GetMethod("get_HealthMgr");
			_Context_Field = EnemyCharacter_TypeDef.GetField("_Context");

			chealthManager_Type = get_HealthMgr_Method.ReturnType.GetType();

			_Em_Field = _Context_Field.GetType().GetField("_Em");

			var enemyContext_TypeDef = _Em_Field.GetType();

			get_IsBoss_Method = enemyContext_TypeDef.GetMethod("get_IsBoss");
			Basic_Field = enemyContext_TypeDef.GetField("Basic");

			Boolean_Type = get_IsBoss_Method.ReturnType.GetType();

			var cEmModuleBasic_TypeDef = Basic_Field.GetType();

			EmID_Field = cEmModuleBasic_TypeDef.GetField("EmID");
			RoleID_Field = cEmModuleBasic_TypeDef.GetField("RoleID");
			LegendaryID_Field = cEmModuleBasic_TypeDef.GetField("LegendaryID");

			var EnemyDef_TypeDef = TDB.Get().GetType("app.EnemyDef");

			NameString_Method = EnemyDef_TypeDef.GetMethod("NameString");

			String_Type = NameString_Method.ReturnType.GetType();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void Hook()
	{
		try
		{
			LogManager.Info("MonsterManager: Hooking...");


			preDoUpdateEndHook = doUpdateEnd_Method.AddHook(false);
			preDoUpdateEndHook.AddPre(OnPreDoUpdateEnd);

			LogManager.Info("MonsterManager: Hooked!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private unsafe PreHookResult OnPreDoUpdateEnd(Span<ulong> args)
	{
		try
		{
			var enemyCharacterPtr = args[1];
			var enemyCharacter = ManagedObject.ToManagedObject(enemyCharacterPtr);


			var _Context = (ManagedObject) _Context_Field.GetDataBoxed(enemyCharacterPtr, false);
			if(_Context == null)
			{
				LogManager.Info("[MonsterManager.OnPreDoUpdateEnd] No enemy context holder");
				return PreHookResult.Continue;
			}

			var _Em = (ManagedObject) _Em_Field.GetDataBoxed((ulong) _Context.Ptr(), false);
			if(_Em == null)
			{
				LogManager.Info("[MonsterManager.OnPreDoUpdateEnd] No enemy context");
				return PreHookResult.Continue;
			}

			var isBoss = (bool?) get_IsBoss_Method.InvokeBoxed(Boolean_Type, _Em, []);
			if(isBoss == null)
			{
				LogManager.Info("[MonsterManager.OnPreDoUpdateEnd] No enemy boss type");
				return PreHookResult.Continue;
			}

			if(isBoss == true)
			{
				var isFound = LargeMonsters.ContainsKey(enemyCharacter);
				if(!isFound)
				{
					LargeMonster largeMonster = new(enemyCharacter);
					LargeMonsters.Add(enemyCharacter, largeMonster);
				}
				else
				{
					var largeMonster = LargeMonsters[enemyCharacter];
					largeMonster.UpdateHealth();
				}
			}

			return PreHookResult.Continue;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return PreHookResult.Continue;
		}
	}
}
