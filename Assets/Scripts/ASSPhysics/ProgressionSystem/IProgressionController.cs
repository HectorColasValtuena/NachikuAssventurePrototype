public interface IProgressionController
{
	void SetValue <T> (string key, T value);
	T GetValue <T> (string key);

	void Clear ();
}
