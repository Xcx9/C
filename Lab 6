#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <iomanip>
#include <cstring>
#include <stdexcept>
#include <vector>
#include <string>

using namespace std;

class DateException : public invalid_argument {
public:
    DateException(const string& msg) : invalid_argument(msg) {}
};


class Date {
private:
    int day;
    int month;
    int year;
public:
    Date(int d = 1, int m = 1, int y = 2000) {
        set(d, m, y);
    }

    void set(int d, int m, int y) {
        if (m < 1 || m > 12)
            throw DateException("Месяц вне диапазона 1–12");
        if (d < 1 || d > 31)
            throw DateException("День вне диапазона 1–31");
        if (y > 2025)
            throw DateException("Год больше 2025, вы что терминатор?");
        day = d; 
        month = m; 
        year = y;
    }

    int getDay() const { return day; }
    int getMonth() const { return month; }
    int getYear() const { return year; }

    bool operator>(const Date& other) const {
        if (year != other.year)   return year > other.year;
        if (month != other.month) return month > other.month;
        return day > other.day;
    }

    friend ostream& operator<<(ostream& os, const Date& d) {
        os << setw(2) << setfill('0') << d.day << "."
            << setw(2) << setfill('0') << d.month << "."
            << d.year;
        os << setfill(' ');
        return os;
    }

    friend istream& operator>>(istream& is, Date& d) {
        int dd, mm, yy;
        char sep1, sep2;
        is >> dd >> sep1 >> mm >> sep2 >> yy;
        if (!is || sep1 != '.' || sep2 != '.')
            throw DateException("Неверный формат даты (дд.мм.гггг), вводите дату через точку плез");
        d.set(dd, mm, yy);
        return is;
    }
};


class Supplier {
private:
    char* firma;
    double raw_material;
    Date delivery_date;
    double payment;

    void copyFrom(const Supplier& other) {
        firma = new char[strlen(other.firma) + 1];
        strcpy(firma, other.firma);
        raw_material = other.raw_material;
        delivery_date = other.delivery_date;
        payment = other.payment;
    }

public:
    Supplier()
        : firma(nullptr), raw_material(0), delivery_date(), payment(0)
    {
        firma = new char[1];
        firma[0] = '\0';
    }
    Supplier(const char* f, double raw, const Date& d, double pay)
        : raw_material(raw), delivery_date(d), payment(pay)
    {
        if (raw < 0 || pay < 0)
            throw invalid_argument("Сумма не может быть отрицательной");
        firma = new char[strlen(f) + 1];
        strcpy(firma, f);
    }
    Supplier(const Supplier& other) {
        copyFrom(other);
    }
    Supplier& operator=(const Supplier& other) {
        if (this != &other) {
            delete[] firma;
            copyFrom(other);
        }
        return *this;
    }
    ~Supplier() {
        delete[] firma;
    }


    double getRaw() const { return raw_material; }
    double getPay() const { return payment; }
    const Date& getDate() const { return delivery_date; }


    friend ostream& operator<<(ostream& os, const Supplier& s) {
        os << "| " << left << setw(15) << s.firma
            << " | " << right << setw(8) << s.raw_material
            << " | " << setw(10) << s.payment
            << " | " << setw(12) << s.delivery_date
            << " |" << endl;
        return os;
    }

    friend istream& operator>>(istream& is, Supplier& s) {
        char buf[100];
        cout << "Название фирмы: ";
        is >> ws;
        is.getline(buf, 100);
        if (!is || strlen(buf) == 0)
            throw invalid_argument("Пустое название фирмы, 'Шараж монтаж' хоть напиши");
        cout << "Сумма поставки (>=0): ";
        double raw; is >> raw;
        if (!is || raw < 0) throw invalid_argument("Неверная сумма поставки");
        cout << "Оплата сырья (>=0): ";
        double pay; is >> pay;
        if (!is || pay < 0) throw invalid_argument("Неверная оплата");
        cout << "Дата поставки (дд.мм.гггг): ";
        Date d;
        is >> d;


        delete[] s.firma;
        s.firma = new char[strlen(buf) + 1];
        strcpy(s.firma, buf);
        s.raw_material = raw;
        s.payment = pay;
        s.delivery_date = d;
        return is;
    }


    bool isUnderpaid() const {
        return raw_material > payment;
    }
};


void printHeader() {
    cout << "*=================*==========*================*=============*" << endl;
    cout << "| Фирма           |  Сырье   |  Оплата        | Дата        |" << endl;
    cout << "*=================*==========*================*=============*" << endl;
}

void printFooter() {
    cout << "*=================*==========*================*==============*" << endl;
}

bool is_valid_integer(const std::string& str) {
    size_t pos = 0;
    int num;

    try {
        num = std::stoi(str, &pos);
    }
    catch (...) {  // Ловим любые исключения (invalid_argument или out_of_range)
        return false;
    }

    // Проверяем, что вся строка была обработана (нет лишних символов)
    return pos == str.size();
}

int main() {
    system("chcp 1251");
    vector<Supplier> suppliers;
    vector<int> foundIndices;
    Date cutoff;

    cout << "Система учёта поставщиков:" << endl;
    cout << "Загружено объектов: " << suppliers.size() << endl;

    string choice;
    while (true) {
        try {
            cout << "\n    Главное меню    \n"
                << "1. Ввести поставщиков\n"
                << "2. Распечатать всю информацию\n"
                << "3. Показать неполную оплату\n"
                << "4. Найти поставки позже заданной даты\n"
                << "0. Выход\n"
                << "Выберите пункт: ";
            
            cin >> choice;
            
            if (!cin) throw invalid_argument("Некорректный ввод пункта меню");

            if (choice == "0") {
                break;
            }

            else if (choice == "1") {
                cout << "Сколько поставщиков добавить? ";
                string n; 
                cin >> n;
                int d;
                if (is_valid_integer(n)) {
                    d = stoi(n);
                    if (!cin || d <= 0) throw invalid_argument("Неверное количество");
                    for (int i = 0; i < d; ++i) {
                        cout << "\n    Поставщик №" << (i + 1) << "    \n";
                        Supplier s;
                        cin >> s;
                        suppliers.push_back(s);
                    }
                }
                else {
                    std::cout << "Ошибка: ввод не является числом!\n";
                }
                
            }
            else if (choice == "2") {
                if (suppliers.empty()) {
                    cout << "Нет данных для вывода.\n";
                }
                printHeader();
                for (auto& s : suppliers) cout << s;
                printFooter();
            }
            else if (choice == "3") {
                foundIndices.clear();
                for (size_t i = 0; i < suppliers.size(); ++i) {
                    if (suppliers[i].isUnderpaid()) foundIndices.push_back(i);
                }
                if (foundIndices.empty()) {
                    cout << "Нет поставщиков с неполной оплатой.\n";
                }
                else {
                    cout << "Поставщики с неполной оплатой:\n";
                    printHeader();
                    for (int idx : foundIndices) cout << suppliers[idx];
                    printFooter();
                }
            }
            else if (choice == "4") {
                cout << "Введите пороговую дату (дд.мм.гггг): ";
                cin >> cutoff;
                printHeader();
                bool any = false;
                for (auto& s : suppliers) {
                    if (s.getDate() > cutoff) {
                        cout << s;
                        any = true;
                    }
                }
                if (!any) {
                    cout << "|       Нет записей позже " << cutoff << "       |\n";
                }
                printFooter();
            }
            else {
                cout << "Неверный пункт меню.\n";
            }
        }
        catch (const DateException& ex) {
            cout << "Ошибка даты: " << ex.what() << "\n";
            cin.clear(); cin.ignore(10000, '\n');
        }
        catch (const exception& ex) {
            cout << "Ошибка: " << ex.what() << "\n";
            cin.clear(); cin.ignore(10000, '\n');
        }
    }
    return 0;
}
