using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Common;
using WebApplication1.Entities;

namespace WebApplication1.Data;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (dbContext.Database.IsNpgsql()) dbContext.Database.Migrate();

        var roles = new[] { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = "admin@gmail.com";
        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                Name = "Admin",
                Age = 21,
                Weight = 52,
                Height = 175,
                Goal = "ksmgpksmgpsdfmgp"
            };
            await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        if (!await dbContext.DailyAdvices.AnyAsync())
        {
            var healthTips = new[]
            {
                "Eat at least 5 servings of fruits and vegetables daily.",
                "Drink plenty of water — at least 8 glasses a day.",
                "Limit added sugar and sugary drinks.",
                "Avoid trans fats and reduce saturated fat intake.",
                "Choose whole grains over refined grains.",
                "Eat lean protein (chicken, fish, legumes).",
                "Don’t skip breakfast.",
                "Control portion sizes.",
                "Reduce salt/sodium intake.",
                "Avoid highly processed foods.",
                "Snack on nuts, seeds, or fruit instead of chips or candy.",
                "Include omega-3-rich foods (salmon, walnuts).",
                "Cook more at home to control ingredients.",
                "Use herbs/spices instead of salt for flavor.",
                "Limit red meat; choose plant-based options sometimes.",
                "Avoid late-night eating.",
                "Eat mindfully and slowly.",
                "Check nutrition labels.",
                "Get enough fiber (fruits, veggies, beans, oats).",
                "Don’t drink your calories — avoid excessive juices/sodas.",
                "Aim for at least 150 minutes of moderate exercise weekly.",
                "Include strength training 2+ days per week.",
                "Stretch daily to improve flexibility.",
                "Walk whenever possible.",
                "Take the stairs instead of the elevator.",
                "Stand and move during work breaks.",
                "Try a fitness class or activity you enjoy.",
                "Use resistance bands or bodyweight at home.",
                "Set fitness goals and track progress.",
                "Balance cardio and strength training.",
                "Aim for 7–9 hours of quality sleep nightly.",
                "Maintain a regular sleep schedule.",
                "Avoid screens 1 hour before bed.",
                "Create a calm bedtime routine.",
                "Keep your bedroom cool and dark.",
                "Limit caffeine in the afternoon/evening.",
                "Avoid large meals before bed.",
                "Don’t oversleep on weekends.",
                "Try a sleep app or tracker.",
                "Address snoring or sleep issues with a doctor.",
                "Practice daily gratitude.",
                "Meditate or use mindfulness techniques.",
                "Spend time in nature regularly.",
                "Talk to friends/family about your feelings.",
                "Take breaks from social media.",
                "Journal your thoughts or emotions.",
                "Seek therapy or counseling if needed.",
                "Listen to calming music.",
                "Don’t bottle up stress — manage it constructively.",
                "Practice saying “no” to avoid burnout.",
                "Wash your hands regularly.",
                "Brush teeth twice daily.",
                "Floss daily.",
                "Shower regularly and care for your skin.",
                "Wear sunscreen daily, even in winter.",
                "Avoid touching your face frequently.",
                "Disinfect your phone and workspace.",
                "Trim nails and maintain grooming habits.",
                "Replace toothbrush every 3–4 months.",
                "Use safe, non-toxic personal care products.",
                "Get regular health check-ups.",
                "Monitor blood pressure, cholesterol, and glucose.",
                "Stay up to date with vaccines.",
                "Do cancer screenings (e.g., mammogram, colonoscopy) as advised.",
                "Get eye exams and dental checkups.",
                "Know your family medical history.",
                "Use supplements only if needed (consult your doctor).",
                "Self-examine skin for unusual spots.",
                "Carry a basic first aid kit when traveling.",
                "Know the signs of heart attack or stroke.",
                "Don’t smoke or vape.",
                "Limit alcohol consumption.",
                "Don’t use recreational drugs.",
                "Avoid excessive caffeine.",
                "Drive sober and always wear a seatbelt.",
                "Use hearing protection in loud environments.",
                "Use protective gear for sports/work.",
                "Avoid sitting for long periods.",
                "Don’t self-medicate unnecessarily.",
                "Avoid toxic or negative relationships.",
                "Keep your living space clean and clutter-free.",
                "Reduce screen time.",
                "Grow plants at home to purify the air.",
                "Practice eco-friendly habits (recycle, reduce waste).",
                "Organize your time to reduce stress.",
                "Maintain a good work-life balance.",
                "Take regular vacations or breaks.",
                "Develop healthy hobbies (reading, gardening, art).",
                "Avoid multitasking excessively.",
                "Limit exposure to harmful chemicals at home/work.",
                "Stay connected with loved ones.",
                "Volunteer or help others.",
                "Join a group or community.",
                "Express appreciation often.",
                "Surround yourself with positive people.",
                "Laugh often — humor heals.",
                "Practice forgiveness.",
                "Don’t compare yourself to others.",
                "Be kind to yourself — practice self-compassion.",
                "Celebrate small victories in your health journey."
            };

            var advices = healthTips.Select(tip => new DailyAdvice { Text = tip });
            await dbContext.DailyAdvices.AddRangeAsync(advices);
            await dbContext.SaveChangesAsync();
        }
    }
}
