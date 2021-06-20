using System;

namespace Common
{
    public enum Status
    {
        Inactive,
        Active,
        Complete,
        New
    }
    public enum WorkItemType
    {
        General,
        UserStory,
        Task,
        Bug,
        Issue
    }
    public enum Activity
	{
        Deployment,
        Design,
        Development,
        Documentation,
        Requirments,
        Testing
	}
}
