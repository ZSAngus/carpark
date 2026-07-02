namespace CarPark.Lib;

public interface IUCDetail
{
	ConfigActions AllowedAct { get; }

	bool Loaded { get; set; }

	void Browse();

	void Delete();

	void Detail();

	void Edit();

	void LoadInfo();

	void New();

	void Print();

	void Printpreview();

	void Save();
}
