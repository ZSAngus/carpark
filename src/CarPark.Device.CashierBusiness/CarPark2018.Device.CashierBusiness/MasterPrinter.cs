using System.Drawing;
using System.Drawing.Printing;

namespace CarPark2018.Device.CashierBusiness;

public class MasterPrinter
{
	private PrintDocument mPrintDocument;

	private string mPrintData;

	private string mFontType;

	private int mFontSize;

	public MasterPrinter()
	{
		mPrintDocument = new PrintDocument();
		mPrintDocument.PrintPage += mPrintDocument_PrintPage;
		mPrintDocument.PrintController = new StandardPrintController();
		mFontType = "宋体";
		mFontSize = 9;
	}

	public MasterPrinter(int fontsize, string printer)
	{
		mPrintDocument = new PrintDocument();
		mPrintDocument.PrintPage += mPrintDocument_PrintPage;
		mPrintDocument.PrintController = new StandardPrintController();
		mFontType = "宋体";
		mFontSize = fontsize;
		mPrintDocument.PrinterSettings.PrinterName = printer;
	}

	private void mPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
	{
		Graphics graphics = e.Graphics;
		graphics.DrawString(mPrintData, new Font(mFontType, mFontSize, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 0f, 0f);
	}

	public void OpenDrawer()
	{
	}

	public void Print(string toPrint)
	{
		SendCommand(toPrint);
	}

	private void CutPaper()
	{
		SendCommand("\u001bd\u0002");
	}

	private void SendCommand(string data)
	{
		SetData(data);
		lock (mPrintDocument)
		{
			mPrintDocument.Print();
		}
	}

	public PrintDocument GetPrintDocument()
	{
		return mPrintDocument;
	}

	public void SetData(string data)
	{
		lock (mPrintDocument)
		{
			mPrintData = data;
		}
	}
}
