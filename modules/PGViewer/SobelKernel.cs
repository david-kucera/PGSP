namespace PGViewer
{
	public static class SobelKernel
	{
		#region Constants
		public static int[,] X
		{
			get
			{
				return new int[,]
				{
					{ -1, 0, 1 },
					{ -2, 0, 2 },
					{ -1, 0, 1 }
				};
			}
		}

		public static int[,] Y
		{
			get
			{
				return new int[,]
				{
					{ -1, -2, -1 },
					{ 0, 0, 0 },
					{ 1, 2, 1 }
				};
			}
		}
		#endregion // Constants
	}
}
