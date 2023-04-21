using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ZTest01
{
	[XmlRoot("GetScfCollateralResp")]
	public class GetScfCollateralResp
	{
		[XmlElement("Result")]
		public Result Result { get; set; }

		[XmlElement("Err")]
		public Err Err { get; set; }
	}

	public class Result
	{
		[XmlElement("CollateralID")]
		public string CollateralID { get; set; }
	}

	public class Err
	{
		[XmlElement("errCode")]
		public string errCode { get; set; }
	}

	[Serializable()]
	public class Car
	{
		[System.Xml.Serialization.XmlElement("StockNumber")]
		public string StockNumber { get; set; }

		[System.Xml.Serialization.XmlElement("Make")]
		public string Make { get; set; }

		[System.Xml.Serialization.XmlElement("Model")]
		public string Model { get; set; }
	}


	[Serializable()]
	[System.Xml.Serialization.XmlRoot("CarCollection")]
	public class CarCollection
	{
		[XmlArray("Cars")]
		[XmlArrayItem("Car", typeof(Car))]
		public Car[] Car { get; set; }
	}

	public class Test
	{
		public String value1;
		public String value2;
	}

}
