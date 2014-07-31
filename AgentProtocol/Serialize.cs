﻿/* This file is automatically generated from AgentProtocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null. Don't modify it. */
#if !COMPILE_PROTOCOL
#pragma warning disable 1591
using System;
using System.Collections.Generic;
using EasyGame;


	/// <exclude/>
	public static class SerializeExtension
	{
	}
	/// <exclude/>
	public class IAgentServerProxy : ProtocolAsyncProxy<IAgentServerCallback>, IAgentServer
	{
		public IAgentServer Call { get { return this; } }
		protected override byte[] BuildProtocolHead(ProtocolFlag flags)
		{
			byte[] buf = new byte[16];
			Util.Write(buf, 0, (int)ProtocolSignature.EasyGameV1);
			Util.Write(buf, 4, 2);
			Util.Write(buf, 8, 1);
			Util.Write(buf, 12, (uint)flags);
			return buf;
		}
		public IAgentServerProxy(ProtocolFlag flags = 0) : base(flags)
		{
			RegisterMethod(0, OnError_0);
			RegisterMethod(1, OnNewMail_1);
			RegisterMethod(2, GetMails_2);
		}

		#region CallbackStub
		void GetMails_2(BinaryStreamReader reader)
		{
			int id;
			int ret;
			reader.Read(out id);
			reader.Read(out ret);
			if(ret == 0)
			{
				string[] p0;
				reader.Read(out p0);
				var callback = PopAsyncRequest(id);
				var reply = callback.Reply as Action<string[]>;
				reply(p0);
			}
			else
			{
				var callback = PopAsyncRequest(id);
				string msg;
				reader.Read(out msg);
				if(callback.Error != null)
					callback.Error(ret, msg);
				else
					_handler.OnError(ret, msg);
			}
		}
		void OnError_0(BinaryStreamReader __reader)
		{
			int errCode;
			string errMsg;
			__reader.Read(out errCode);
			__reader.Read(out errMsg);
		#if (DEBUG || LOG_PROTOCOL)
			Log.Debug("OnError errCode: {0}, errMsg: {1}", errCode, errMsg);
		#endif
			_handler.OnError(errCode, errMsg);
		}
		void OnNewMail_1(BinaryStreamReader __reader)
		{
			string mail;
			__reader.Read(out mail);
		#if (DEBUG || LOG_PROTOCOL)
			Log.Debug("OnNewMail mail: {0}", mail);
		#endif
			_handler.OnNewMail(mail);
		}
		#endregion
		#region Methods
		void IAgentServer.Login(string userId)
		{
			BinaryStreamWriter __stream = new BinaryStreamWriter();
			__stream.Write(1);
			__stream.Write(userId);
			Connection.Write(__stream.Buffer, __stream.Position);
		}
		void IAgentServer.GetMails(Action<string[]> OnResults)
		{
			BinaryStreamWriter __stream = new BinaryStreamWriter();
			__stream.Write(2);
			__stream.Write(AddAsyncRequest(OnResults, null));
			Connection.Write(__stream.Buffer, __stream.Position);
		}
		#endregion
	}
	/// <exclude/>
	public static class IAgentServerCallbackProxy
	{
		public static void OnError(Connection connection, int errCode, string errMsg)
		{
			if(connection == null)
				return;
			BinaryStreamWriter __stream = new BinaryStreamWriter();
			__stream.Write(0);
			__stream.Write(errCode);
			__stream.Write(errMsg);
		#if (DEBUG || LOG_PROTOCOL)
			Log.Debug("send({0}) OnError errCode: {1}, errMsg: {2}", __stream.Position, errCode, errMsg);
		#endif
			connection.Write(__stream.Buffer, __stream.Position);
		}
		public static void OnNewMail(Connection connection, string mail)
		{
			if(connection == null)
				return;
			BinaryStreamWriter __stream = new BinaryStreamWriter();
			__stream.Write(1);
			__stream.Write(mail);
		#if (DEBUG || LOG_PROTOCOL)
			Log.Debug("send({0}) OnNewMail mail: {1}", __stream.Position, mail);
		#endif
			connection.Write(__stream.Buffer, __stream.Position);
		}
	}
#endif
