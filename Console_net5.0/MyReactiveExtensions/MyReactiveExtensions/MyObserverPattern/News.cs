namespace MyReactiveExtensions
{
	/// <summary>
	/// 配信するニュースを定義
	/// </summary>
	public class News
	{
		public string Title { get; private set; }
		public string Content { get; private set; }

		public News(string title, string content)
		{
			Title = title;
			Content = content;
		}
	}
}