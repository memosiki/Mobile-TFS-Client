using Newtonsoft.Json;
using System;

namespace App5.Models.DTO
{
    public partial class ApiItem
    {
        [JsonProperty("queryType")]
        public string QueryType { get; set; }

        [JsonProperty("queryResultType")]
        public string QueryResultType { get; set; }

        [JsonProperty("asOf")]
        public DateTimeOffset AsOf { get; set; }

        [JsonProperty("columns")]
        public Column[] Columns { get; set; }

        [JsonProperty("workItems")]
        public WorkItem[] WorkItems { get; set; }
    }

    public partial class Column
    {
        [JsonProperty("referenceName")]
        public string ReferenceName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class WorkItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }


    //////////////////////
    public partial class WorkItemInstance
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("rev")]
        public long Rev { get; set; }

        [JsonProperty("fields")]
        public Fields Fields { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Fields
    {
        [JsonProperty("System.AreaPath")]
        public string SystemAreaPath { get; set; }

        [JsonProperty("System.TeamProject")]
        public string SystemTeamProject { get; set; }

        [JsonProperty("System.IterationPath")]
        public string SystemIterationPath { get; set; }

        [JsonProperty("System.WorkItemType")]
        public string SystemWorkItemType { get; set; }

        [JsonProperty("System.State")]
        public string SystemState { get; set; }

        [JsonProperty("System.Reason")]
        public string SystemReason { get; set; }

        [JsonProperty("System.AssignedTo")]
        public SystemAssignedToClass SystemAssignedTo { get; set; }

        [JsonProperty("System.CreatedDate")]
        public DateTimeOffset SystemCreatedDate { get; set; }

        [JsonProperty("System.CreatedBy")]
        public SystemAssignedToClass SystemCreatedBy { get; set; }

        [JsonProperty("System.ChangedDate")]
        public DateTimeOffset SystemChangedDate { get; set; }

        [JsonProperty("System.ChangedBy")]
        public SystemAssignedToClass SystemChangedBy { get; set; }

        [JsonProperty("System.CommentCount")]
        public long SystemCommentCount { get; set; }

        [JsonProperty("System.Title")]
        public string SystemTitle { get; set; }

        [JsonProperty("System.BoardColumn")]
        public string SystemBoardColumn { get; set; }

        [JsonProperty("System.BoardColumnDone")]
        public bool SystemBoardColumnDone { get; set; }

        [JsonProperty("Microsoft.VSTS.Common.StateChangeDate")]
        public DateTimeOffset MicrosoftVstsCommonStateChangeDate { get; set; }

        [JsonProperty("Microsoft.VSTS.Common.Priority")]
        public long MicrosoftVstsCommonPriority { get; set; }

        [JsonProperty("WEF_98AF844AB34044DFBDE58130E2FA9F64_Kanban.Column")]
        public string Wef98Af844Ab34044Dfbde58130E2Fa9F64KanbanColumn { get; set; }

        [JsonProperty("WEF_98AF844AB34044DFBDE58130E2FA9F64_Kanban.Column.Done")]
        public bool Wef98Af844Ab34044Dfbde58130E2Fa9F64KanbanColumnDone { get; set; }

        [JsonProperty("System.Description")]
        public string SystemDescription { get; set; }
    }

    public partial class SystemAssignedToClass
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("uniqueName")]
        public string UniqueName { get; set; }

        [JsonProperty("imageUrl")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }
    }

    public partial class Avatar
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }
    }


}
