using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models
{
    public class LineChartModel
    {
        public string Name { get; set; }

        public string Label { get; set; }
        public DateTime Date { get; set; }

        public double Value { get; set; }

        public double Value1 { get; set; }

        public double Size { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public bool IsSummary { get; set; }

        public string Department { get; set; }

        public List<double> EmployeeAges { get; set; }

        public LineChartModel(string department, List<double> employeeAges)
        {
            Department = department;
            EmployeeAges = employeeAges;
        }

        public LineChartModel(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public LineChartModel(string name, double value, double horizontalErrorValue, double verticalErrorValue)
        {
            Name = name;
            Value = value;
            High = horizontalErrorValue;
            Low = verticalErrorValue;
        }

        public LineChartModel(string name, double value, double size)
        {
            Name = name;
            Value = value;
            Size = size;
        }

        public LineChartModel(double value, double value1, double size)
        {
            Value1 = value;
            Value = value1;
            Size = size;
        }

        public LineChartModel(double value1, double value, double size, string label)
        {
            Value1 = value1;
            Value = value;
            Size = size;
            Label = label;
        }

        public LineChartModel(string name, double high, double low, double open, double close)
        {
            Name = name;
            High = high;
            Low = low;
            Value = open;
            Size = close;
        }
        public LineChartModel(DateTime date, double high, double low, double open, double close)
        {
            Date = date;
            High = high;
            Low = low;
            Value = open;
            Size = close;
        }
        public LineChartModel(double value, double size)
        {
            Value = value;
            Size = size;
        }
        public LineChartModel(DateTime dateTime, double value)
        {
            Date = dateTime;
            Value = value;
        }

        public LineChartModel(string name, double value, bool isSummary)
        {
            Name = name;
            Value = value;
            IsSummary = isSummary;
        }
    }
}
