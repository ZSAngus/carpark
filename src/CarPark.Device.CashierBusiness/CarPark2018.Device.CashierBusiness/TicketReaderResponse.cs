using System;
using System.Text;

namespace CarPark2018.Device.CashierBusiness;

public class TicketReaderResponse
{
	private string m_Response;

	public int Length => m_Response.Length + 2;

	public string Response => m_Response;

	public TicketReaderResponse()
	{
		m_Response = string.Empty;
	}

	public static TicketReaderResponse FromBytes(byte[] bytes)
	{
		TicketReaderResponse result = null;
		try
		{
			TicketReaderResponse ticketReaderResponse = new TicketReaderResponse();
			ticketReaderResponse.m_Response = Encoding.ASCII.GetString(bytes, 1, bytes.Length - 2);
			TicketReaderResponse ticketReaderResponse2 = ticketReaderResponse;
			result = ticketReaderResponse2;
		}
		catch (Exception)
		{
		}
		return result;
	}
}
