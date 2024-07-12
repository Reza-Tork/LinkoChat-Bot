using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Application.Utils
{
    public class Constants
    {
        public class Texts
        {
            public const string ERROR = @"متوجه نشدم :/
    
<code>چه کاری برات انجام بدم؟ از منوی پایین انتخاب کن 👇</code>";
            public const string IN_CHAT_ERROR = @"<b>⚠️ خطا : هم اکنون شما در حال چت هستید !</b>

برای استفاده از ربات ابتدا باید مکالمه رو قطع کنی 👇";

            public const string HELLO_START = @"سلام {0} عزیز ✋️

به 《لینکو چت 🤖》 خوش اومدی ، توی این ربات می تونی افراد #نزدیک ات رو پیدا کنی و باهاشون آشنا شی و یا به یه نفر بصورت #ناشناس وصل شی و باهاش #چت کنی ❗️

- استفاده از این ربات رایگانه و اطلاعات تلگرام شما مثل اسم،عکس پروفایل یا موقعیت GPS کاملا محرمانه هست😎";

            public const string REGISTER_COMPLETE = @"✅اطلاعات شما ثبت شد.

به خانواده بزرگ #لینکو_چت خوش اومدی بهت توصیه میکنم اول از همه با لمس کردن 《🤔 راهنما》 با ربات آشنا شی

<code>از منوی پایین👇 انتخاب کن</code>";

            public const string COMPLETE_PROFILE_REWARD = @"راستی میدونستی توی لینکو چت🤖》  یه پروفایل داری؟ 

برای دیدن پروفایل خودت گزینه 《👤 پروفایل》 رو از منو انتخاب کن 👇

💥 با تکمیل کردن اطلاعات پروفایلت 💰 5 تا سکه رایگان بگیر 😍";

            public const string GENDER_SELECT = "<code>برای شروع جنسیتت رو انتخاب کن 👇</code>";
            public const string GENDER_BOY = "من 🙍‍♂️پسرم";
            public const string GENDER_GIRL = "من 🙎‍♀️دخترم";
            public const string GENDER_ERROR = "⚠️ خطا: فقط یکی از گزینه های زیر را انتخاب کنید 👇";

            public const string AGE_SELECT = @"خب حالا سنت رو بهم بگو ؟

<code>• سنت رو از لیست پایین 👇انتخاب کن یا خودت تایپ کن</code>";
            public const string AGE_ERROR = @"⚠️ خطا: فقط یکی از گزینه های زیر را انتخاب یا فقط عدد را وارد کنید 👇";


            public const string STATE_SELECT = @"خب حالا فقط کافیه استانت رو انتخاب کنی تا وارد ربات شیم

<code>• استانت رو از لیست پایین 👇انتخاب کن</code>";
            public const string STATE_ERROR = @"⚠️ خطا: فقط یکی از گزینه های زیر را انتخاب کنید 👇";

            public const string CITY_SELECT = @"❓ لطفا شهر خود را انتخاب کنید 👇";
            public const string PROFILE_PICTURE_SELECT = @"❓ لطفا عکس پروفایل خود را ارسال کنید.";
            public const string LOCATION_SELECT = @"⚠️ هنگام ارسال موقعیت مکانی مطمعن شوید GPS موبایل شما روشن است.

✅ کسی قادر به دیدن موقعیت مکانی شما در ربات نخواهد بود و فقط برای تخمین فاصله و یافتن افراد نزدیک کاربرد خواهد داشت

<a href='https://t.me/Rezaa_trk'>📚 گیف آموزشی ارسال موقعیت GPS با 2 روش متفاوت ( کلیک کنید)</a>

❓موقعیت GPS خود را ارسال کنید👇";
            public const string LOCATION_COMPLETE = "✏️ تغییر موقعیت با موفقیت انجام شد ✅";
            public const string INVITER_COIN = @"🔔 تبریک !

شما {0} سکه بابت {1} کاربری که توسط شما معرفی شده بود دریافت کردید.

💰سکه فعلی شما : {2}";
        }

        public class KeyboardButtons
        {
            public const string CONNECT_RANDOM = @"🔗 به یه ناشناس وصلم کن!";
            public const string NEAR_PEOPLE = "📍افراد نزدیک";
            public const string SEARCH_PEOPLE = "🔍 جستجوی کاربران 🔎";
            public const string HELP = "🤔راهنما";
            public const string PROFILE = "👤پروفایل";
            public const string COIN = "💰سکه";
            public const string INVITE = "🚸 معرفی به دوستان (سکه رایگان)";
            public const string REQUEST_USER_PROFILE = "👀مشاهده پروفایل این مخاطب👤";
            public const string ENABLE_PRIVTE_CHAT = "فعال سازی چت خصوصی 🔐";
            public const string END_CHAT = "پایان چت";

        }

        public class InlineKeyboardButtons
        {
            public const string COMPLETE_PROFILE = "💥تکمیل پروفایل و دریافت جایزه💥";

            public const string RANDOM_SEARCH = "🎲 جستجوی شانسی 🎲";
            public const string RANDOM_SEARCH_BOY = "🤵‍♀️ جستجوی دختر";
            public const string RANDOM_SEARCH_GIRL = "🤵‍♂️ جستجوی پسر";
            public const string CANCEL_SEARCH = @"❌ انصراف از جستجو ❌";

            public const string NEAR_SEARCH_BOY = "فقط 🤵‍♂️ پسر ها";
            public const string NEAR_SEARCH_GIRL = "فقط 🤵‍♀️ دختر ها";
            public const string NEAR_SEARCH_ALL = "همه رو نشون بده";

            public const string SEARCH_SAME_STATE = "🚩 هم استانی ها";
            public const string SEARCH_SAME_AGE = "👥 هم سنی ها";
            public const string SEARCH_ADVANCE = "🔍 جستجوی پیشرفته 🔎";
            public const string SEARCH_NEW = "🙋‍♀️ کاربران جدید 🙋‍♂️";
            public const string SEARCH_NOCHATS = "🚶‍♂️ بدون چت ها 🚶‍♀️";
            public const string SEARCH_MY_LAST = "👀 چت های اخیر من 👀";

            public const string MYPROFILE_MY_GPS = "📍 مشاهده موقعیت GPS من";
            public const string MYPROFILE_CAN_LIKE = "لایک ({0})";
            public const string MYPROFILE_WHOLIKED = "♥️ مشاهده لایک کننده ها";
            public const string MYPROFILE_BLOCKED = "🚫 بلاک شده ها";
            public const string MYPROFILE_CONTACTS = "🙍‍♂️👩 لیست مخاطبین";
            public const string MYPROFILE_EDIT = "📝 ویرایش اطلاعات پروفایل";

            public const string PROFILE_LIKES = "Like{0} {1}";
            public const string PROFILE_BUY_COIN = "خرید سکه برای کاربر";
            public const string PROFILE_REQUEST_CHAT = "💬 درخواست چت";
            public const string PROFILE_REQUEST_DIRECT = "🗒 پیام دایرکت";
            public const string PROFILE_REPORT = "🚫 گزارش کاربر";
            public const string PROFILE_BLOCK = "🔒 بلاک کردن کاربر";
            public const string PROFILE_UNBLOCK = "🔐 آنبلاک کردن کاربر";
            public const string PROFILE_ADD_CONTACT = "➕ افزودن به مخاطبین";
            public const string PROFILE_ONLINE_ALERT = "🔔 به محض انلاین شدن به من اطلاع بده";

            public const string COIN_RANDOM = "🎲 سکه شانسی امروز 🎲";
            public const string COIN_INVITE = "🚸 معرفی به دوستان (سکه رایگان)";
            public const string COIN_ITEM = "{0} سکه {1} تومان {2}";
        }

        public class MessagesText
        {
            public const string RANDOM_SEARCH = @"<b>به کی وصلت کنم؟</b>
<code>انتخاب کن👇</code>";
            public const string NEAR_SEARCH = @"🛰 چه کسایی رو نشونت بدم؟
<code>انتخاب کن👇</code>";
            public const string HELP = @"🔹راهنمای استفاده از ربات:

<code>من اینجام که کمکت کنم! برای دریافت راهنمایی در مورد هر موضوع، کافیه دستور آبی رنگی که مقابل اون سوال هست رو لمس کنی:</code>

⁉️ چگونه بصورت ناشناس چت کنم؟ /help_chat

⁉️ سکه یا امتیاز چیست؟ /help_credit

⁉️ چگونه افراد نزدیکمو پیدا کنم؟ /help_gps

⁉️ پروفایل چیست؟ /help_profile

⁉️ چگونه درخواست چت بفرستم؟ /help_sendchat

⁉️ پیام دایرکت چیست؟ /help_direct

⁉️ چگونه با 'میان بر' ها کار کنم؟ /help_shortcuts

⁉️ اطلاع رسانی آنلاین شدن مخاطب /help_onw

⁉️ اطلاع رسانی اتمام چت مخاطب /help_chw

⁉️ لیست مخاطبین چیست ؟ /help_contacts

⁉️ چگونه بصورت پیشرفته بین کاربران جستجو کنم ؟ /help_search

⁉️ آموزش حذف پیام در چت /help_deleteMessage

⚖️ قوانین استفاده از ربات /ghavanin


👨‍💻 ارتباط با پشتیبانی ربات: @LinkoChat_Support";
            public const string MYPROFILE = @"• نام: {0}
• جنسیت: {1}
• استان: {2}
• شهر: {3}
• سن: {4}

♥️ لایک ها : {5}

هم اکنون 👀 آنلایـــن

🆔 آیدی : /user_{6}

🔔 تنظیم حالت سایلنت: /silent";
            public const string USERPROFILE = @"• نام: {0}
• جنسیت: {1}
• استان: {2}
• شهر: {3}
• سن: {4}

⏳ {5}

🆔 آیدی : /user_{6}


🏁 فاصله از شما: {7}";
            public const string COIN = @"💰سکه فعلی شما : {0}
ــــــــــــــــــــــــــــــــــــــــ

❓روش های بدست آوردن سکه چیست؟

1️⃣ معرفی دوستان (رایگان) :

برای افزایش سکه به صورت رایگان بنر لینک⚡️ مخصوص خودت (/link) رو برای دوستات  بفرست و 20 سکه دریافت کن

2️⃣ خرید سکه بصورت آنلاین :

برای خرید سکه یکی از تعرفه های زیر را انتخب نمایید👇";
            public const string INVITE_BANNER = @"《مِلو گپ 🤖》 هستم،بامن میتونی

📡افراد نزدیک یا 👫هم سن خودت رو پیدا کنی و بصورت ناشناس چت کنی...

 ➕ میتونی از هر شهری که دلت بخواد دوست مجازی پیدا کنی و کلی امکانت دیگه...😎

همین الان رو لینک بزن 👇
http://t.me/Melogap4bot?start={0}

✅ #رایگان و کاملا #واقعی 😎";
            public const string INVITE_BANNER_GENERATED = @"لینک⚡️ دعوت شما با موفقیت ساخته شد 👆

<code>شما میتوانید بنر حاوی لینک⚡️ خود را به گـــروه ها و دوستان خود ارسال کنید</code>

- با معرفی هر نفر 20 سکه بگیرید!برای اطلاعات بیشتر راهنمای سکه(/help_credit)را بخوانید.

👈 شما تاکنون 0 نفر را به این ربات دعوت کرده اید .";
            public const string SEARCHING_NOW = @"🔎 درحال جستجوی مخاطب ناشناس شما 
- <code>جستجوی {0}</code>

⏳ حداکثر تا ۲ دقیقه صبر کنید.

⚙️ جستجوی همسن : 📴 غیر فعال
- فعال کردن : /hamseni_on

⚙️ جستجوی هم استانی ها: 📴 غیر فعال
- فعال کردن : /hamostani_on";
        }
    }
}
