namespace Identity.Application.Common;

public class Result<T>
{
	public T? Value { get; }
	public bool IsFailed { get; }

	private Result(T? value)
	{
		Value = value;
		IsFailed = false;
	}

	private Result()
	{
		Value = default(T);
		IsFailed = true;
	}

	public static Result<T> Success(T value) => new(value);
	public static Result<T> Fail() => new();
}
