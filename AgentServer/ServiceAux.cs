﻿/* This file is automatically generated from AgentProtocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null. Don't modify it. */
#if !COMPILE_PROTOCOL
using System;
using System.Collections.Generic;
using EasyGame;


	/// <exclude/>
	[ServiceAuxiliary(Type = typeof(IAgentServer), Stub = typeof(IAgentServerStub))]
	public interface IAgentServerImpl
	{
		void Login(Session session, string userId);
	}
	/// <exclude/>
	public class IAgentServerStub : StubBase
	{
		public IAgentServerStub()
		{
			AddMethodDispatcher(1, Login_1);
		}

		static BinaryStreamWriter Login_1(object __serviceObj, Session __client, BinaryStreamReader __reader)
		{
			var __timer = TimeCounter.BeginNew();
			IAgentServerImpl __service = (IAgentServerImpl)__serviceObj;
			string userId;
			__reader.Read(out userId);
		#if (DEBUG || LOG_PROTOCOL)
			Log.Debug("Login userId: {0}", userId);
		#endif
			__service.Login(__client, userId);
			PerfStatistic.AddItem("IAgentServer.Login", (int)__timer.End());
			return null;
		}
	}
#endif
