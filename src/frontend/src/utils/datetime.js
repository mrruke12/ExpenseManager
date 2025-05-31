export const formatDateRange = (date1, date2, lang) => {
  const months = {
    ru: [
      'января', 'февраля', 'марта', 'апреля', 'мая', 'июня',
      'июля', 'августа', 'сентября', 'октября', 'ноября', 'декабря'
    ],
    en: [
      'January', 'February', 'March', 'April', 'May', 'June',
      'July', 'August', 'September', 'October', 'November', 'December'
    ]
  };

  const day1 = date1.getDate();
  const month1 = months[lang][date1.getMonth()];
  const year1 = date1.getFullYear();

  const day2 = date2.getDate();
  const month2 = months[lang][date2.getMonth()];
  const year2 = date2.getFullYear();

  if (year1 === year2 && date1.getMonth() === date2.getMonth()) {
    return lang === 'ru' 
      ? `${day1} — ${day2} ${month1} ${year1}`
      : `${day1} — ${day2} ${month1}, ${year1}`;
  } else if (year1 === year2) {
    return lang === 'ru'
      ? `${day1} ${month1} — ${day2} ${month2} ${year1}`
      : `${day1} ${month1} — ${day2} ${month2}, ${year1}`;
  } else {
    return lang === 'ru'
      ? `${day1} ${month1} ${year1} — ${day2} ${month2} ${year2}`
      : `${day1} ${month1}, ${year1} — ${day2} ${month2}, ${year2}`;
  }
}

export const getPeriodDates = (period, _milestone) => {
    const date = new Date(_milestone); // Клонируем, чтобы не менять исходную дату
    let firstDate, lastDate;

    switch (period) {
        case 'all':
            const from = new Date(date.getFullYear() - 100, 11, 31);
            from.setHours(0, 0, 0, 0);
            firstDate = from;

            const to = new Date(date.getFullYear() + 100, 0, 1);
            to.setHours(0, 0, 0, 0);
            lastDate = to;
            break;
        case 'week':
            // Начало: последний день предыдущей недели (воскресенье)
            const prevSunday = new Date(date);
            prevSunday.setDate(date.getDate() - date.getDay());
            prevSunday.setHours(0, 0, 0, 0);
            firstDate = prevSunday;

            // Конец: первый день следующей недели (понедельник)
            const nextMonday = new Date(date);
            nextMonday.setDate(date.getDate() + (7 - date.getDay()));
            nextMonday.setHours(0, 0, 0, 0);
            lastDate = nextMonday;
            break;

        case 'month':
            // Начало: последний день предыдущего месяца
            const lastDayPrevMonth = new Date(date.getFullYear(), date.getMonth(), 0);
            lastDayPrevMonth.setHours(0, 0, 0, 0);
            firstDate = lastDayPrevMonth;

            // Конец: первый день следующего месяца
            const firstDayNextMonth = new Date(date.getFullYear(), date.getMonth() + 1, 1);
            firstDayNextMonth.setHours(0, 0, 0, 0);
            lastDate = firstDayNextMonth;
            break;

        case 'year':
            // Начало: последний день предыдущего года
            const lastDayPrevYear = new Date(date.getFullYear() - 1, 11, 31);
            lastDayPrevYear.setHours(0, 0, 0, 0);
            firstDate = lastDayPrevYear;

            // Конец: первый день следующего года
            const firstDayNextYear = new Date(date.getFullYear() + 1, 0, 1);
            firstDayNextYear.setHours(0, 0, 0, 0);
            lastDate = firstDayNextYear;
            break;

        default:
            throw new Error('Invalid period. Use "week", "month", or "year".');
    }

    return { from: firstDate, to: lastDate, milestone: new Date(_milestone) };
}

export const dateToString = (date, locale) => {
    if (!(date instanceof Date)) {
        date = new Date(date);
    }

    const options = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        weekday: 'long',
    };

    if (locale === 'ru') {
        return date.toLocaleDateString('ru-RU', {
            ...options,
            month: 'long',
            weekday: 'long',
        });
    }

    return date.toLocaleDateString('en-US', {
        ...options,
        month: 'long',
        weekday: 'long',
    });
}