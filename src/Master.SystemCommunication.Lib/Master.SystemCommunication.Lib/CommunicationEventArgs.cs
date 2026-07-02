using System;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 定义一个本例的事件消息类. 创建包含有关事件的其他有用的信息的变量，只要派生自EventArgs即可。
/// </summary>
public class CommunicationEventArgs : EventArgs
{
	public MessageType msgType { get; set; }

	public ParkingSpacesChangeNoticeArgs parkingSpacesChangeNoticeArgs { get; set; }

	public DeviceStatus deviceStatus { get; set; }

	public NoticeArgs noticeArgs { get; set; }
}
