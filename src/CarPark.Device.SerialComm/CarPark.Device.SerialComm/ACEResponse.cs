using System;
using System.Text;

namespace CarPark.Device.SerialComm;

public class ACEResponse : BaseCommand
{
	private string m_ACK;

	private string m_DATA;

	private string m_GateID;

	public string ACK => m_ACK;

	public string GateID => m_GateID;

	public string String_0 => m_DATA;

	public ACEResponse()
	{
		Class2.WKJkUh2zLspup();
		m_ACK = string.Empty;
		m_DATA = string.Empty;
		m_GateID = string.Empty;
	}

	public ACEResponse(string ack, string data, string gateid)
	{
		Class2.WKJkUh2zLspup();
		m_ACK = string.Empty;
		m_DATA = string.Empty;
		m_GateID = string.Empty;
		m_ACK = ack;
		m_DATA = data;
		m_GateID = gateid;
	}

	public ACEResponse Clone()
	{
		return new ACEResponse(m_ACK, m_DATA, m_GateID);
	}

	public static ACEResponse FromBytes(byte[] received)
	{
		try
		{
			ACEResponse aCEResponse = new ACEResponse();
			aCEResponse.m_ACK = Encoding.ASCII.GetString(received, 5, 1);
			aCEResponse.m_DATA = Encoding.ASCII.GetString(received, 6, received.Length - 8);
			aCEResponse.m_GateID = Encoding.ASCII.GetString(received, 1, 3);
			return aCEResponse;
		}
		catch (Exception)
		{
			return null;
		}
	}
}
