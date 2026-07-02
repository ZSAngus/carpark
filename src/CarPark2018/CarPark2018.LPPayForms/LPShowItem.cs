using System.Drawing;

namespace CarPark2018.LPPayForms;

public class LPShowItem
{
	public string Licenseplate { get; set; }

	public string Intime { get; set; }

	public Image Image { get; set; }

	public LPShowItem(string lp, string intime, Image image)
	{
		Licenseplate = lp;
		Intime = intime;
		Image = image;
	}
}
