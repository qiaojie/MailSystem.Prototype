MailSystem.Prototype
====================

邮件的订阅分发原型程序。

* MailProtocol —— 邮件服务器的协议
* AgentProtocol —— 用户Agent服务器协议
* MailServer —— 邮件服务器
* AgentServer —— Agent服务器
* TestClient —— 测试客户端

运行Run.bat即可启动4个测试进程，在MailServer上运行命令行: SendMail username mailcontent 即可向用户发送邮件，如果用户在线则会收到邮件通知。
