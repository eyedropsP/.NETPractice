using System;

namespace ExecutionContext
{
	/// <summary>
	/// 静的フィールド
	/// 静的フィールドでは共有範囲としては広すぎて使えない場合が多い
	/// </summary>
	public class MyStaticField
	{
		private static int StaticField;

		public void Execute()
		{
			StaticField = 123;

			var x = StaticField;
			
			// x にはStaticFieldの値が代入されている
			// 共有範囲内だから123が入ってる
			Console.WriteLine($"StaticField x : {x}");
		}
	}
}