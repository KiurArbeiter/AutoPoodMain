﻿using Autopood.Models.Car;
using System.ComponentModel.DataAnnotations;

namespace Autopood.Domain
{
    public class Car
    {
        [Key]
        public Guid? Id { get; set; }
        public string Mark { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Register { get; set; }
        public string SerialNumber { get; set; }
        public string Engine { get; set; }
        public int Tires { get; set; }
        public int Mileage { get; set; }
        public int Seats { get; set; }
        public bool Inspection { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
