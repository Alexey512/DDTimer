namespace Assets.Scripts.Common
{
	public enum CustomObjectStatus
	{
		Undefined,
		Empty,
		Valid
	}

	public class CustomObject
	{
		private static readonly CustomObject _empty = new CustomObject();

		public object Value { get; private set; }
		public string ErrorMessage { get; set; }

		public CustomObjectStatus Status { get; private set; }

		public static CustomObject Empty => _empty;

		public CustomObject(object value)
		{
			Status = value != null ? CustomObjectStatus.Valid : CustomObjectStatus.Empty;
			Value = value;
			ErrorMessage = string.Empty;
		}

		private CustomObject()
		{
			Status = CustomObjectStatus.Empty;
			Value = null;
			ErrorMessage = string.Empty;
		}
	}
}
