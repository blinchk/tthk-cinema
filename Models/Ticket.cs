﻿using System;
using tthk_kinoteater.Interfaces;

namespace tthk_kinoteater.Models
{
    internal class Ticket : IPlace
    {
        public int Id { get; set; }
        public Hall Hall => Session.Hall;
        public Session Session { get; set; }
        public DateTime StartTime => Session.StartTime;
        public DateTime EndTime => Session.EndTime;
        public int Number { get; set; }
        public int Row { get; set; }
        public double TicketAdditionalCost => 0.10 * Row;
    }
}