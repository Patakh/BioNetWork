using Microsoft.AspNetCore.Authorization;

namespace BioNetWork.Model.HR.Policy;

public class HRManagerRobationRequirement : IAuthorizationRequirement
{
    public HRManagerRobationRequirement(int probationMonths)
    {
        ProbationMonths = probationMonths;
    }
    public int ProbationMonths { get; }
}
public class HRManangetProbationrequirementHandler : AuthorizationHandler<HRManagerRobationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerRobationRequirement requirement)
    {
        if (!context.User.HasClaim(x => x.Type == "EmploymentDate"))
            return Task.CompletedTask;
        if (DateTime.TryParse(context.User.FindFirst(x => x.Type == "EmploymentDate")?.Value, out DateTime emloyementDate))
        {
            var period = DateTime.Now - emloyementDate;
            if (period.Days > 30 * requirement.ProbationMonths)
                context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}


