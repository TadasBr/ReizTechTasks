Console.WriteLine("Please insert hours (0 - 12):");
int hours = Convert.ToInt16(Console.ReadLine());

Console.WriteLine("Please insert minutes (0 - 60):");
int minutes = Convert.ToInt16(Console.ReadLine());

Console.WriteLine("Angle between hour and minute arrows: {0} degrees", CalculateAngle(hours, minutes));

int CalculateAngle(int hours, int minutes)
{
    if(hours < 0 || minutes < 0 || hours > 12 || minutes > 60)
    {
        Console.WriteLine("Wrong input");
        return 0;
    }

    hours = hours == 12 ? 0 : hours;

    if(minutes == 60)
    {
        minutes = 0;
        hours += 1;
        hours = hours > 12 ? hours - 12 : hours;
    }

    int hour_angle = (int)(0.5 * (hours * 60 + minutes));
    int minute_angle = (int)(6 * minutes);

    int angle = Math.Abs(hour_angle - minute_angle);

    angle = Math.Min(360 - angle, angle);

    return angle;
}