public interface IFingerUpManager
{
	bool FingerUp { get; set; }
	void ExecuteFingerUpEvent( bool _fingerUp );
}
