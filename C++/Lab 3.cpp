#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <iomanip>
#include <cstring>
using namespace std;

class date {
private:
    int day;
    int month;
    int year;

public:
    date(int d = 1, int m = 1, int y = 2000) : day(d), month(m), year(y) {}

    friend ostream& operator<<(ostream& os, const date& dt) {
        os << setw(2) << setfill('0') << dt.day << "." 
           << setw(2) << setfill('0') << dt.month << "." 
           << dt.year;
        return os;
    }

    bool operator==(const date& other) const {
        return day == other.day && month == other.month && year == other.year;
    }
};

class Student {
private:
    char* fam;
    char* name;
    date birthday;
    int group;
    static int count;

public:
    Student() : fam(nullptr), name(nullptr), group(0) {
        count++;
    }

    Student(const char* f, const char* n, const date& bd, int g) : group(g), birthday(bd) {
        fam = strdup(f);
        name = strdup(n);
        count++;
    }

    Student(const Student& other) : birthday(other.birthday), group(other.group) {
        fam = strdup(other.fam);
        name = strdup(other.name);
        count++;
    }

    ~Student() {
        free(fam);
        free(name);
        count--;
    }

    Student& operator=(const Student& other) {
        if (this != &other) {
            free(fam);
            free(name);
            fam = strdup(other.fam);
            name = strdup(other.name);
            birthday = other.birthday;
            group = other.group;
        }
        return *this;
    }

    bool operator==(const date& dt) const {
        return birthday == dt;
    }

    friend ostream& operator<<(ostream& os, const Student& s) {
        os << setw(15) << left << s.fam 
           << setw(15) << left << s.name 
           << setw(15) << left << s.birthday 
           << setw(5) << left << s.group;
        return os;
    }

    static int getCount() { return count; }
};

int Student::count = 0;

int main() {
    system("chcp 1251");
    
    // 1. Добавление нескольких новых элементов
    int n = 3;
    Student* students = new Student[n] {
        Student("Иванов", "Иван", date(15, 5, 2000), 101),
        Student("Петров", "Петр", date(20, 8, 2001), 102),
        Student("Сидоров", "Алексей", date(15, 5, 2000), 103)
    };

    // 2. Вывод данных с помощью перегруженного оператора <<
    cout << "Список всех студентов:\n";
    cout << setw(15) << left << "Фамилия" 
         << setw(15) << left << "Имя" 
         << setw(15) << left << "Дата рождения" 
         << setw(5) << left << "Группа" << endl;
    cout << string(50, '-') << endl;
    
    for (int i = 0; i < n; i++) {
        cout << students[i] << endl;
    }
    cout << endl;

    // 3. Поиск студентов с заданным днем рождения
    date search_date(15, 5, 2000);
    Student* Rez = new Student[n];
    int rez_count = 0;

    for (int i = 0; i < n; i++) {
        if (students[i] == search_date) {
            Rez[rez_count++] = students[i];
        }
    }

    // 4. Вывод результатов в табличном виде
    cout << "Студенты с днем рождения " << search_date << ":\n";
    cout << setw(15) << left << "Фамилия" 
         << setw(15) << left << "Имя" 
         << setw(15) << left << "Дата рождения" 
         << setw(5) << left << "Группа" << endl;
    cout << string(50, '-') << endl;
    
    for (int i = 0; i < rez_count; i++) {
        cout << Rez[i] << endl;
    }

    cout << "\nВсего студентов: " << Student::getCount() << endl;

    delete[] students;
    delete[] Rez;
    return 0;
}
