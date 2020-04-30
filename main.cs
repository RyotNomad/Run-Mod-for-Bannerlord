using System;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.InputSystem;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace RunMod
{
	public class Main : MBSubModuleBase
	{
		protected override void OnSubModuleLoad()
		{
			string fullPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\"));
			string text = File.ReadAllText(fullPath + "/Settings.cfg");
			string[] array = text.Split(new char[]
			{
				'\n'
			});

			for (int i = 0; i < array.Length; i++)
			{
				bool flag = array[i].StartsWith("//");
				if (!flag)
				{
					this.sprintKey = (InputKey)int.Parse(array[i]);
				}
			}



		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002170 File Offset: 0x00000370
		protected override void OnApplicationTick(float dt)
		{
			bool flag = Game.Current == null;
			if (!flag)
			{
				bool flag2 = Game.Current.CurrentState > 0;
				if (!flag2)
				{
					bool flag3 = Mission.Current == null || Mission.Current.Scene == null;
					if (!flag3)
					{

						bool setDef = false;
						Agent main = Agent.Main;
						

						bool flag5 = main == null;
						if (!flag5)
						{
							
							main.SetMaximumSpeedLimit(0.6f, true);


							if (!setDef)
							{
								main.SetMaximumSpeedLimit(0.5f, true);
								foreach (Agent agent in Mission.Current.AllAgents)
								{
									
									if (!agent.IsMount)
									{
										agent.SetMaximumSpeedLimit(0.6f, true);
									}
									else
									{
										agent.SetMaximumSpeedLimit(280f, false);
									}
								}
							}
							//InputKey temp = (InputKey)42;
							//InputKey tempr = (InputKey)36;			


							if (Input.IsKeyDown(sprintKey))
							{
								main.SetMaximumSpeedLimit(5f, true);


							}
						}
					}
				}
			}
		}



		private Dictionary<string, string> settings = new Dictionary<string, string>();


		private InputKey sprintKey = InputKey.V;




	}
}

