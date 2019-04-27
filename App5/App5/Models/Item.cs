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
        public string Icon { get; set; } = null;
        public static string GetIcon(string category)
        {
            switch (category)
            {
                case "Issue":
                    return "issue.png";
                case "Epic":
                    return "epic.png"; 
                case "Task":
                    return "task.png";
                default:
                    return "issue.png"; 
            }
        }
    }
}