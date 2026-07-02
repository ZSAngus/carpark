using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CarPark2018.Device.CashierBusiness;

[Serializable]
public class CashierBusinessConfig
{
	public ComSettings FeeLED { get; set; }

	public ComSettings FeePrinter { get; set; }

	public ComSettings FeeSmartCard { get; set; }

	public ComSettings FeeTicketOperator { get; set; }

	public ComSettings FeeMPassPOS { get; set; }

	public int CounterRFIDReaderMode { get; set; }

	public ComSettings QRScaner { get; set; }

	public int TicketType { get; set; }

	public ComSettings FeeQPassPOS { get; set; }

	public ComSettings QRScanerPay { get; set; }

	public static CashierBusinessConfig FromString(string xmlString)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(CashierBusinessConfig));
		using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString));
		return xmlSerializer.Deserialize(stream) as CashierBusinessConfig;
	}

	public override string ToString()
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(CashierBusinessConfig));
		using MemoryStream memoryStream = new MemoryStream();
		xmlSerializer.Serialize(memoryStream, this);
		return Encoding.UTF8.GetString(memoryStream.ToArray());
	}
}
