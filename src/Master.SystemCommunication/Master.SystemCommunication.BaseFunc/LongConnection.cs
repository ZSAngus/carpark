using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;
using System.Timers;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.BaseFunc;

public class LongConnection : ILongConnection
{
	private static object syncObj = new object();

	public static ConcurrentDictionary<string, ILongConnectionCallBack> ListenCalls = new ConcurrentDictionary<string, ILongConnectionCallBack>();

	public static Timer listenTimer = null;

	public static bool Listening = false;

	public static int ListenInterval = 1;

	protected ProgramInfo m_programInfo;

	/// <summary>
	/// 客戶端斷線事件
	/// </summary>
	public static event Action<string> ClientLeavesEvent;

	public LongConnection()
	{
		Console.WriteLine("new LongConnection");
		Listening = true;
		AutoListenPrograms();
	}

	public static void AutoListenPrograms()
	{
		List<string> Leaves = new List<string>();
		if (listenTimer == null)
		{
			listenTimer = new Timer(ListenInterval * 60 * 1000);
			listenTimer.Elapsed += delegate(object sender, ElapsedEventArgs e)
			{
				Timer timer = sender as Timer;
				try
				{
					timer.Stop();
					Leaves.Clear();
					foreach (KeyValuePair<string, ILongConnectionCallBack> listenCall in ListenCalls)
					{
						ILongConnectionCallBack value = listenCall.Value;
						try
						{
							value.Listen(listenCall.Key);
							Console.WriteLine("Listen pass:" + listenCall.Key);
						}
						catch
						{
							Leaves.Add(listenCall.Key);
							Console.WriteLine("Listen Faulted:" + listenCall.Key);
						}
					}
					if (Leaves.Count > 0)
					{
						foreach (string item in Leaves)
						{
							ILongConnectionCallBack value2 = null;
							lock (syncObj)
							{
								try
								{
									ListenCalls.TryRemove(item, out value2);
									if (LongConnection.ClientLeavesEvent != null)
									{
										LongConnection.ClientLeavesEvent(item);
									}
								}
								catch
								{
								}
							}
						}
						return;
					}
				}
				finally
				{
					if (Listening)
					{
						timer.Start();
					}
				}
			};
		}
		else
		{
			listenTimer.Interval = ListenInterval * 60 * 1000;
		}
		listenTimer.Start();
	}

	public virtual bool Join(ProgramInfo programInfo)
	{
		if (!string.IsNullOrEmpty(programInfo.OnlyID))
		{
			Console.WriteLine("Join:" + programInfo.OnlyID);
			m_programInfo = programInfo;
			ILongConnectionCallBack listenCall = OperationContext.Current.GetCallbackChannel<ILongConnectionCallBack>();
			lock (syncObj)
			{
				ListenCalls.AddOrUpdate(programInfo.OnlyID, listenCall, (string key, ILongConnectionCallBack value) => listenCall);
			}
			return true;
		}
		Console.WriteLine("Join OnlyID is null or Empty !");
		return false;
	}

	public void RunListen()
	{
		Console.WriteLine("RunListen");
	}
}
