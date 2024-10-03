using Marten.Schema;

namespace Catalog.API.Features.Category.Common;

public sealed class InitialDataCategory : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Entities.Category>().AnyAsync(cancellation))
            return;

        session.Store<Entities.Category>(GetCategories());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Entities.Category> GetCategories() => new List<Entities.Category>
    {
        new Entities.Category{ Id = 1, Name = "لباس", Description = "انواع لباس‌ها از جمله مردانه، زنانه و بچگانه." },
        new Entities.Category { Id = 2, Name = "کفش", Description = "مجموعه‌ای از کفش‌های مردانه، زنانه و ورزشی." },
        new Entities.Category { Id = 3, Name = "کیف و کوله‌پشتی", Description = "انواع کیف‌های دستی، دوشی و کوله‌پشتی." },
        new Entities.Category { Id = 4, Name = "لوازم الکترونیک", Description = "تلفن‌های همراه، تبلت‌ها و لوازم جانبی." },
        new Entities.Category { Id = 5, Name = "لوازم خانگی", Description = "کالاهای برقی و غیر برقی برای خانه." },
        new Entities.Category { Id = 6, Name = "مبلمان", Description = "انواع مبلمان برای خانه و محل کار." },
        new Entities.Category { Id = 7, Name = "دکوراسیون", Description = "اقلام تزئینی و لوازم دکوراسیون داخلی." },
        new Entities.Category { Id = 8, Name = "آشپزخانه", Description = "ابزار و لوازم مورد نیاز برای آشپزخانه." },
        new Entities.Category { Id = 9, Name = "کتاب", Description = "کتاب‌های چاپی در موضوعات مختلف." },
        new Entities.Category { Id = 10, Name = "لوازم تحریر", Description = "ابزار و وسایل نوشت‌افزاری برای مدرسه و محل کار." },
        new Entities.Category { Id = 11, Name = "ورزشی", Description = "لباس‌ها و لوازم مورد نیاز برای ورزش‌های مختلف." },
        new Entities.Category { Id = 12, Name = "زیورآلات", Description = "گردنبند، دستبند و زیورآلات دیگر." },
        new Entities.Category { Id = 13, Name = "ساعت", Description = "انواع ساعت‌های مچی و دیواری." },
        new Entities.Category { Id = 14, Name = "اسباب بازی", Description = "انواع اسباب‌بازی‌های کودکان." },
        new Entities.Category { Id = 15, Name = "لوازم نوزاد", Description = "محصولات و لوازم مورد نیاز برای نوزادان." },
        new Entities.Category { Id = 16, Name = "آرایشی و بهداشتی", Description = "محصولات آرایشی، بهداشتی و مراقبت از پوست." },
        new Entities.Category { Id = 17, Name = "عطر و ادکلن", Description = "انواع عطرها و ادکلن‌ها." },
        new Entities.Category { Id = 18, Name = "لوازم خودرو", Description = "ابزارها و لوازم یدکی خودرو." },
        new Entities.Category { Id = 19, Name = "بازی و سرگرمی", Description = "بازی‌های فکری و سرگرمی‌های دیگر." },
        new Entities.Category { Id = 20, Name = "موسیقی", Description = "ابزارها و لوازم موسیقی." },
        new Entities.Category { Id = 21, Name = "کامپیوتر و لپ‌تاپ", Description = "انواع کامپیوتر و لپ‌تاپ و لوازم جانبی آنها." },
        new Entities.Category { Id = 22, Name = "دوربین و فیلم‌برداری", Description = "دوربین‌های عکاسی، فیلم‌برداری و لوازم مرتبط." },
        new Entities.Category { Id = 23, Name = "بازی‌های ویدئویی", Description = "کنسول‌های بازی و بازی‌های ویدئویی." },
        new Entities.Category { Id = 24, Name = "ابزار و تجهیزات صنعتی", Description = "ابزارآلات و تجهیزات صنعتی." },
        new Entities.Category { Id = 25, Name = "حیوانات خانگی", Description = "لوازم و غذای حیوانات خانگی." },
        new Entities.Category { Id = 26, Name = "دوچرخه و اسکوتر", Description = "انواع دوچرخه‌ها و اسکوترها." },
        new Entities.Category { Id = 27, Name = "لوازم سفر", Description = "تجهیزات و لوازم سفر و کمپینگ." },
        new Entities.Category { Id = 28, Name = "اکسسوری", Description = "اکسسوری‌های مد و فشن مانند کلاه، شال و دستکش." },
        new Entities.Category { Id = 29, Name = "محصولات ارگانیک", Description = "محصولات ارگانیک و طبیعی." },
        new Entities.Category { Id = 30, Name = "گل و گیاه", Description = "گل‌ها و گیاهان آپارتمانی و باغچه‌ای." },
        new Entities.Category { Id = 31, Name = "لوازم ورزشی", Description = "تجهیزات ورزشی و تناسب اندام." },
        new Entities.Category { Id = 32, Name = "مبلمان اداری", Description = "مبلمان و تجهیزات دفتری." },
        new Entities.Category { Id = 33, Name = "نرم‌افزار", Description = "نرم‌افزارهای کاربردی و بازی‌های کامپیوتری." },
        new Entities.Category { Id = 34, Name = "لوازم الکتریکی", Description = "ابزارها و وسایل الکتریکی و برق‌کاری." },
        new Entities.Category { Id = 35, Name = "محصولات فرهنگی", Description = "فیلم‌ها، سریال‌ها و محصولات فرهنگی دیگر." },
        new Entities.Category { Id = 36, Name = "طلا و جواهر", Description = "انواع طلا و جواهرات." },
        new Entities.Category { Id = 37, Name = "پوشاک ورزشی", Description = "لباس‌ها و کفش‌های ورزشی." },
        new Entities.Category { Id = 38, Name = "صنایع دستی", Description = "محصولات و هنرهای دستی." },
        new Entities.Category { Id = 39, Name = "شیرینی و شکلات", Description = "شیرینی، شکلات و محصولات خوراکی دیگر." },
        new Entities.Category { Id = 40, Name = "مواد غذایی", Description = "مواد غذایی و نوشیدنی‌های مختلف." },
        new Entities.Category { Id = 41, Name = "پوشاک نوزاد", Description = "لباس‌ها و لوازم پوشیدنی برای نوزادان." },
        new Entities.Category { Id = 42, Name = "لوازم آرایشی", Description = "محصولات آرایشی و زیبایی." },
        new Entities.Category { Id = 43, Name = "محصولات دیجیتال", Description = "محصولات دیجیتال و هوشمند." },
        new Entities.Category { Id = 44, Name = "ابزار باغبانی", Description = "ابزار و وسایل مربوط به باغبانی و کشاورزی." },
        new Entities.Category { Id = 45, Name = "محصولات بهداشتی", Description = "محصولات بهداشتی و مراقبتی." },
        new Entities.Category { Id = 46, Name = "ساعت هوشمند", Description = "انواع ساعت‌های هوشمند و دستبندهای سلامت." },
        new Entities.Category { Id = 47, Name = "لوازم جانبی موبایل", Description = "گارد، شارژر و سایر لوازم جانبی موبایل." },
        new Entities.Category { Id = 48, Name = "کنسول بازی", Description = "کنسول‌های بازی و لوازم مرتبط." },
        new Entities.Category { Id = 49, Name = "ظروف آشپزخانه", Description = "ظروف پخت و پز و لوازم سرو غذا." },
        new Entities.Category { Id = 50, Name = "لوازم آرایشگاهی", Description = "ابزار و لوازم آرایشگاه‌ها و سالن‌های زیبایی." }
    };
}