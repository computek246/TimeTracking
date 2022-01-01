using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTracking.Context;
using TimeTracking.Entities;

namespace TimeTracking
{
    public class Program
    {

        private static async Task Main(string[] args)
        {
            var name = Assembly.GetExecutingAssembly().GetName().Name ?? "";
            var now = DateTime.Now;


            Console.Title = name;
            Console.WriteLine(name);

            await using var context = new TimeTrackingDbContext();
            var projects = await context.Projects.ToListAsync();
            int selectedProjectId;

        Start:

            //Console.Clear();
            Console.WriteLine($"\n * Select Project Id from list:\n");

            foreach (var project in projects)
            {
                Console.WriteLine($"\t[{project.Id}] - {project.ProjectName}");
            }

            const string invalidNumber = "Invalid number entered. Please enter valid number";

            if (int.TryParse(Console.ReadLine(), out var result))
            {
                if (projects.Select(x => x.Id).Contains(result))
                {
                    var project = projects.FirstOrDefault(x => x.Id == result);
                    if (project != null)
                    {
                        Console.WriteLine($"[{project.Id}] - {project.ProjectName} selected.");
                        selectedProjectId = result;
                    }
                    else
                    {
                        Console.WriteLine(invalidNumber);
                        goto Start;
                    }
                }
                else
                {
                    Console.WriteLine(invalidNumber);
                    goto Start;
                }
            }
            else
            {
                Console.WriteLine(invalidNumber);
                goto Start;
            }

            try
            {

                var arg = Regex.Replace(string.Join(" ", args), @"\s+", " ");

                if (!string.IsNullOrEmpty(arg))
                {

                    Console.WriteLine($"{arg} at: {now:dd/MM/yyyy hh:mm:ss tt}");

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

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;

                if (setting?.Value != null)
                    Console.WriteLine(setting.Value, now.Year);
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception);
            }
            finally
            {

                Console.ResetColor();
                Console.WriteLine("Press any key to exit . . .");
                Console.ReadLine();
            }
        }
    }
}
