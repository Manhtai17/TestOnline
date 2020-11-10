using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entitty
{
	public class CheckDoExam : Contest
	{
		/// <summary>
		/// check show điểm hay button làm bài
		/// true-hiện nút làm bài, false- hiện kết quả thi
		/// </summary>
		public bool Doing { get; set; }
	}
}
