using System;

namespace App5.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public string State { get; set; } = "To Do";
        public string AssignedTo { get; set; } = "";
        public string Category { get; set; } = "Issue";
    }
}