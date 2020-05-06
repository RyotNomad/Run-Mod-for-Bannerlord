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
					this.settings.Add(array[i].Split(new char[]
					{
						'='
					})[0], array[i].Split(new char[]
					{
						'='
					})[1]);
				}
			}
			this.sprintKey = (InputKey)int.Parse(this.settings["SprintKey"]);
			this.backKey = (InputKey)int.Parse(this.settings["BackKey"]);
			this.leftKey = (InputKey)int.Parse(this.settings["LeftKey"]);
			this.rightKey = (InputKey)int.Parse(this.settings["RightKey"]);




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

							if (!isRunning)
							{
								main.SetMaximumSpeedLimit(0.55f, true);
							}

							if (!setDef)
							{
								if (!isRunning)
								{
									main.SetMaximumSpeedLimit(0.55f, true);
								}
								foreach (Agent agent in Mission.Current.AllAgents)
								{
									if (agent != main)
									{
										if (!agent.IsMount)
										{
											agent.SetMaximumSpeedLimit(0.55f, true);
										}
										else
										{
											agent.SetMaximumSpeedLimit(280f, false);
										}
									}
								}
							}
							//InputKey temp = (InputKey)42;
							//InputKey tempr = (InputKey)36;			


							if (Input.IsKeyPressed(sprintKey))
							{
								if (isRunning)
								{
									main.SetMaximumSpeedLimit(0.55f, true);
									isRunning = false;
								}
								//I know bad practice but it looks nicer
								else if (!isRunning)
								{
									main.SetMaximumSpeedLimit(5f, true);
									isRunning = true;
								}
							}

							else if (Input.IsKeyDown( backKey))
							{
								//	InformationManager.DisplayMessage(new InformationMessage(main.GetCurrentSpeedLimit().ToString()));
								if (!isRunning)
								{
									main.SetMaximumSpeedLimit(0.279f, true);
								}
							}

							else if (Input.IsKeyDown(leftKey))
							{
								//InformationManager.DisplayMessage(new InformationMessage(main.GetCurrentSpeedLimit().ToString()));
								if (!isRunning)
								{
									main.SetMaximumSpeedLimit(0.403f, true);
								}
							}
							else if (Input.IsKeyDown(rightKey))
							{
								//InformationManager.DisplayMessage(new InformationMessage(main.GetCurrentSpeedLimit().ToString()));
								if (!isRunning)
								{
									main.SetMaximumSpeedLimit(0.403f, true);
								}
							}
						}
					}
				}
			}
		}



		private Dictionary<string, string> settings = new Dictionary<string, string>();

		private bool isRunning = false;

		private InputKey sprintKey = InputKey.V;
		private InputKey leftKey = InputKey.A;
		private InputKey rightKey = InputKey.D;
		private InputKey backKey = InputKey.S;





	}
}
