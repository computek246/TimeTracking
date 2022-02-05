using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTracking.Common.Constant;
using TimeTracking.Domain.Context;
using TimeTracking.Domain.Entities;

namespace TimeTracking.Console
{
    public class Program
    {

        private static async Task Main(string[] args)
        {
            var name = Assembly.GetExecutingAssembly().GetName().Name ?? "";
            var now = DateTime.Now;


            System.Console.Title = name;
            System.Console.WriteLine(name);

            await using var context = new TimeTrackingDbContext();
            var projects = await context.Projects.ToListAsync();
            int selectedProjectId;

        Start:

            //Console.Clear();
            System.Console.WriteLine($"\n * Select Project Id from list:\n");

            foreach (var project in projects)
            {
                System.Console.WriteLine($"\t[{project.Id}] - {project.ProjectName}");
            }

            const string invalidNumber = "Invalid number entered. Please enter valid number";

            if (int.TryParse(System.Console.ReadLine(), out var result))
            {
                if (projects.Select(x => x.Id).Contains(result))
                {
                    var project = projects.FirstOrDefault(x => x.Id == result);
                    if (project != null)
                    {
                        System.Console.WriteLine($"[{project.Id}] - {project.ProjectName} selected.");
                        selectedProjectId = result;
                    }
                    else
                    {
                        System.Console.WriteLine(invalidNumber);
                        goto Start;
                    }
                }
                else
                {
                    System.Console.WriteLine(invalidNumber);
                    goto Start;
                }
            }
            else
            {
                System.Console.WriteLine(invalidNumber);
                goto Start;
            }

            try
            {

                var arg = Regex.Replace(string.Join(" ", args), @"\s+", " ");

                if (!string.IsNullOrEmpty(arg))
                {

                    System.Console.WriteLine($"{arg} at: {now:dd/MM/yyyy hh:mm:ss tt}");

                    await ResilientTransaction.New(context)
                        .ExecuteAsync(async () =>
                        {
                            var log = new ActionsLog
                            {
                                ActionId = null,
                                ActionName = arg,
                                ActionDate = now,
                                ProjectId = selectedProjectId
                            };

                            await context.AddAsync(log);
                            await context.SaveChangesAsync();
                        });

                }

                var setting = await context.Settings.FirstOrDefaultAsync(x =>
                    x.Name == SettingValues.GeneralSetting.LegalCopyright);

                System.Console.BackgroundColor = ConsoleColor.DarkBlue;
                System.Console.ForegroundColor = ConsoleColor.White;

                if (setting?.Value != null)
                    System.Console.WriteLine(setting.Value, now.Year);
            }
            catch (Exception exception)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(exception);
            }
            finally
            {

                System.Console.ResetColor();
                System.Console.WriteLine("Press any key to exit . . .");
                System.Console.ReadLine();
            }
        }
    }
}
