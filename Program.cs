using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UserPhone.Base;
using UserPhone.Apps;
using Social;
using Global;

namespace UserPhone
{
    public class Phone
    {
        private bool enabled; //can the phone do anything
        private bool active; //the user is engaged with the phone.
        private PhoneMain main;
        public bool ProcessEngagement(PhoneEngagement e)
        {

            return false;
        }
        public void ToggleActive()
        {


        }
        public void ToggleEnable()
        {


        }
        public void update()
        {


        }
        public void init()
        {
            main = new PhoneMain(new HardwareInterface(new Storage(), new Audio(), new Camera()), new Screen());
        }
        private class Screen
        {
            private Image image;
            public void update()
            {

            }
            public void tap()
            {

            }
            private void draw()
            {

            }

        }
        private class PhoneMain
        {
            private App activeApp;
            private HardwareInterface hi;
            public PhoneMain(HardwareInterface hi, Screen s)
            {
                this.hi = hi;
            }
            public void switchApp(App a)
            {

            }
        }
    }
}
namespace UserPhone.Base
{
    public class Storage
    {
        public List<Setting> settings;
        public List<App> apps;
        public List<AppDataFolder> folders;
        public List<Contact> contacts;
        public List<Photo> photos;
    }
    public interface StorageItem
    {

    }
    public abstract class AppDataFolder
    {
        public List<StorageItem> contents;
    }
    public class Contact : StorageItem
    {
        private Image thumbnail;
        private Character character;
    }
    public class Photo : StorageItem
    {
        private Image image;
        private Date date;
        bool taken; // was the photo taken by the user?
    }
    public class AlertSound : StorageItem
    {

    }
    public interface Setting : StorageItem
    {

    }
    public class HardwareInterface
    {
        public Storage s;
        public Audio a;
        public Camera c;
        public HardwareInterface(Storage s, Audio a, Camera c)
        {
            this.s = s;
            this.a = a;
            this.c = c;
        }
    }
    public class Image
    {

    }
    public interface PhoneEngagement
    {

    }
    public class Date
    {


    }
    public class Camera
    {

    }
    public class Audio
    {

    }
    public class PhoneCall : PhoneEngagement
    {

    }
    public class TextMessage : PhoneEngagement
    {

    }
}

namespace UserPhone.Apps
{
    public abstract class App
    {
        protected HardwareInterface hardware;
        protected List<Setting> settings;
        public abstract Image update();
        public abstract PhoneEngagement getEngagementType();
        public App(HardwareInterface h)
        {
            hardware = h;
        }
    }
    public class Phone : App
    {
        public override Image update()
        {
            return new Image();
        }
        public override PhoneEngagement getEngagementType()
        {
            return new PhoneCall();
        }
        public Phone(HardwareInterface h) : base(h)
        {

        }
    }
    public class Messenger : App
    {
        public override Image update()
        {
            return new Image();
        }
        public override PhoneEngagement getEngagementType()
        {
            return new TextMessage();
        }
        public Messenger(HardwareInterface h) : base(h)
        {

        }
    }
    public class Maps : App
    {
        public override Image update()
        {
            return new Image();
        }
        public override PhoneEngagement getEngagementType()
        {
            return null;
        }
        public Maps(HardwareInterface h) : base(h)
        {

        }
    }
    public class Music : App
    {
        public override Image update()
        {
            return new Image();
        }
        public override PhoneEngagement getEngagementType()
        {
            return null;
        }
        public Music(HardwareInterface h) : base(h)
        {

        }
    }
    public class Settings : App
    {
        public override Image update()
        {
            return new Image();
        }
        public override PhoneEngagement getEngagementType()
        {
            return null;
        }
        public Settings(HardwareInterface h) : base(h)
        {

        }
    }
    public class Camera : App
    {
        public override Image update()
        {
            return new Image();
        }
        public override PhoneEngagement getEngagementType()
        {
            return null;
        }
        public Camera(HardwareInterface h) : base(h)
        {

        }
    }
    public class ContactBook : App
    {
        public override Image update()
        {
            return new Image();
        }
        public override PhoneEngagement getEngagementType()
        {
            return null;
        }
        public ContactBook(HardwareInterface h) : base(h)
        {

        }
    }
}
namespace Social
{
    public class Character
    {
        private string name;
        private List<Emote> emotes;
        private List<SpeechPattern> patterns;
        public List<Relationship> relationships;
    }
    public class Relationship
    {
        public readonly Character c1;
        public readonly Character c2;
        private int friendship;
        private int chemistry;
        public Character getChar1()
        {
            return c1;
        }
        public Character getChar2()
        {
            return c2;
        }
        public int getFriendship()
        {
            return friendship;
        }
        public void addFriendship(int l)
        {
            friendship += l;
        }
        public void subtractFriendship(int l)
        {
            friendship -= l;
        }
        public int getChemistry()
        {
            return friendship;
        }
        public void addChemistry(int l)
        {
            chemistry += l;
        }
        public void subtractChemistry(int l)
        {
            chemistry -= l;
        }
    }
    public class Emote
    {

    }
    public class SpeechPattern
    {

    }
}
namespace Social.Dialouges
{
    public class DialougeObject
    {
        public readonly string text;
        public readonly Condition c;
        public readonly DialougeObject next;
        private GameState state;
    }
    public class DialougeManager
    {
        private Dialouge d;
        private DialougeObject currentObject;
        private GameState state;
        public DialougeObject next()
        {
            while (currentObject.next != null || state.CheckCondition(currentObject.next.c))
                currentObject = currentObject.next;
            return currentObject;
        }
        public DialougeObject getCurrentObject()
        {
            return currentObject;
        }
    }
    public class Dialouge
    {
        public readonly List<DialougeObject> speeches;
    }
    public class Speech
    {
        public readonly Character character;
        public readonly Emote emote;
        public readonly SpeechPattern pattern;
    }
    public class SpeechResponse : Speech
    {
        public readonly List<DialougeOption> options;
    }
    public class DialougeOption : DialougeObject
    {
        public readonly string label;
    }
}
namespace Calendar
{
    public class Date
    {
        private static readonly Dictionary<int, int> DAYS_IN_MONTHS = new Dictionary<int, int> { { 1, 31 }, { 2, 28 },
            { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        private static readonly  Dictionary<int, int> DAYS_IN_MONTHS_LEAP = new Dictionary<int, int> { { 1, 31 }, { 2, 29 },
            { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };

        private int minutes;
        public int getYear()
        {
            int year = 1;
            for (bool stop = false; stop == true; year++)
            {
                if (year % 4 == 0 && minutes > 527040)
                    minutes -= 527040;
                else if (minutes > 525600)
                    minutes -= 525600;
                else
                    stop = true;
            }
            return year;
        }
        public int getMonth()
        {
            int year = 1;
            for (bool stop = false; stop == true; year++)
            {
                if (year % 4 == 0 && minutes > 527040)
                    minutes -= 527040;
                else if (minutes > 525600)
                    minutes -= 525600;
                else
                    stop = true;
            }
            int month = 1;
            Dictionary<int, int> yearType;
            if (year % 4 == 0)
                yearType = DAYS_IN_MONTHS_LEAP;
            else
                yearType = DAYS_IN_MONTHS;
            for (; minutes < yearType[month]; month++)
                minutes -= yearType[i] * 1440;
            return month;
        }
        public int getDay()
        {

            int year = 1;
            for (bool stop = false; stop == true; year++)
            {
                if (year % 4 == 0 && minutes > 527040)
                    minutes -= 527040;
                else if (minutes > 525600)
                    minutes -= 525600;
                else
                    stop = true;
            }
            int month = 1;
            Dictionary<int, int> yearType;
            if (year % 4 == 0)
                yearType = DAYS_IN_MONTHS_LEAP;
            else
                yearType = DAYS_IN_MONTHS;
            for (; minutes < yearType[month]; month++)
                minutes -= yearType[i] * 1440;
            return minutes / 1440;
        }
        public int getHour()
        {
            int year = 1;
            for (bool stop = false; stop == true; year++)
            {
                if (year % 4 == 0 && minutes > 527040)
                    minutes -= 527040;
                else if (minutes > 525600)
                    minutes -= 525600;
                else
                    stop = true;
            }
            int month = 1;
            Dictionary<int, int> yearType;
            if (year % 4 == 0)
                yearType = DAYS_IN_MONTHS_LEAP;
            else
                yearType = DAYS_IN_MONTHS;
            for (; minutes < yearType[month]; month++)
                minutes -= yearType[i] * 1440;
            minutes /= 1440;
            return minutes / 60;
        }
        public int getMinute()
        {
            int year = 1;
            for (bool stop = false; stop == true; year++)
            {
                if (year % 4 == 0 && minutes > 527040)
                    minutes -= 527040;
                else if (minutes > 525600)
                    minutes -= 525600;
                else
                    stop = true;
            }
            int month = 1;
            Dictionary<int, int> yearType;
            if (year % 4 == 0)
                yearType = DAYS_IN_MONTHS_LEAP;
            else
                yearType = DAYS_IN_MONTHS;
            for (; minutes < yearType[month]; month++)
                minutes -= yearType[i] * 1440;
            minutes /= 1440;
            return minutes / 3600;
        }
    }
    public class InGameCalendar
    {

    }
}

namespace Global
{
    public class Constants
    {


    }
    public class GraphicsSettings
    {


    }
    public class GameSettings
    {


    }
    public class Controls
    {

    }
    public class GameState
    {
        private Dictionary<string, object> state;
        public object getState(string s)
        {
            return state[s];
        }
        public bool CheckCondition(Condition c)
        {
            return c.check(state[c.state]);
        }
    }
    public abstract class Condition
    {
        public readonly string state;
        public abstract bool check(object state);
        public Condition(string state)
        {
            this.state = state;
        }
    }
    public class QuantityCondition : Condition
    {
        public readonly string type;
        public readonly float goal;
        public override bool check(object state)
        {
            switch (type)
            {
                case "<":
                    return ((float)state < goal);
                case ">":
                    return ((float)state > goal);
                case "=":
                    return ((float)state == goal);
            }
            return false;
        }
        public QuantityCondition(string state, string type, float goal) : base(state)
        {
            this.type = type;
            this.goal = goal;
        }
    }
    public class BooleanCondition : Condition
    {
        public readonly bool goal;
        public override bool check(object state)
        {
            return (goal == (bool)state);
        }
        public BooleanCondition(string state, bool goal) : base(state)
        {
            this.goal = goal;
        }
    }
    public class StringMatchCondition : Condition
    {
        public readonly string s;
        public readonly bool goal;
        public override bool check(object state)
        {
            return (goal.Equals((string)state));
        }
        public StringMatchCondition(string state, bool goal, string s) : base(state)
        {
            this.goal = goal;
            this.s = s;
        }
    }
}