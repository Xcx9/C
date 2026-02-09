#define _CRT_SECURE_NO_WARNINGS
#include <cstring>
#include <iostream>
#include <fstream>
#include <string>

using namespace std;

class date {
private:
    int day;
    int month;
    int year;
public:
    friend ostream& operator<<(ostream& os, const date& d) {
        os << d.getDay() << "." << d.getMonth() << "." << d.getYear();
        return os;
    }

    bool operator==(const date& d2) const {
        return day == d2.day && month == d2.month && year == d2.year;
    }

    bool operator>(const date& d2) const {
        if (year > d2.year) return true;
        if (year == d2.year && month > d2.month) return true;
        if (year == d2.year && month == d2.month && day > d2.day) return true;
        return false;
    }

    date(int d = 1, int m = 1, int y = 2000) : day(d), month(m), year(y) 
    {}
    int getDay() const { 
        return day; 
    }
    int getMonth() const { 
        return month; 
    }
    int getYear() const { 
        return year; 
    }
};

class FIO {
private:
    char* fam;
    char* name;
public:
    FIO(const char* f = "", const char* n = "") {
        fam = new char[strlen(f) + 1];
        strcpy(fam, f);
        name = new char[strlen(n) + 1];
        strcpy(name, n);
    }
    ~FIO() {
        delete[] fam;
        delete[] name;
    }
    const char* getFam() const { return fam; }
    const char* getName() const { return name; }
};


class Teacher {
private:
    FIO fio;
    date birthday;
    char* course;
public:
    Teacher(const char* fam, const char* name, int d, int m, int y, char* course)
        : birthday(d, m, y), fio(fam, name), course(course) {
        this->course = new char[strlen(course) + 1];
        strcpy(this->course, course);
    }

    ~Teacher() {
        delete[] course;
    }




    FIO getFio() const { return fio; }
    char* getCourse() const { return course; }
    date getBirthday() const { return birthday; }
    friend ostream& operator<<(ostream& os, const Teacher& s);
};




class Student {
private:
    char* fam;
    char* name;
    date birthday;
    int grup;
    static int count;
public:
    Student(const char* fam, const char* name, int d, int m, int y, int grup)
        : birthday(d, m, y), grup(grup) {
        this->fam = new char[strlen(fam) + 1];
        strcpy(this->fam, fam);
        this->name = new char[strlen(name) + 1];
        strcpy(this->name, name);
        count++;
    }

    ~Student() {
        delete[] fam;
        delete[] name;
        count--;
    }

    static int getCount() {
        return count;
    }

    const char* getFam() const { return fam; }
    const char* getName() const { return name; }
    int getGrup() const { return grup; }
    date getBirthday() const { return birthday; }
    friend ostream& operator<<(ostream& os, const Student& s);
};

int Student::count = 0;

ostream& operator<<(ostream& os, const Student& s) {
    os << s.fam << "\t" << s.name << "\t" << s.grup << "\t" << s.birthday;
    return os;
}

bool operator==(const Student& s, const date& d) {
    return s.getBirthday() == d;
}



class Common {
protected:
    FIO fio;
    date birthday;
public:
    Common(const char* fam, const char* name, int d, int m, int y)
        : fio(fam, name), birthday(d, m, y) {}
    virtual ~Common() {}

    virtual void print(ostream& os) const = 0;
    friend ostream& operator<<(ostream& os, const Common& c) {
        c.print(os);
        return os;
    }

    date getBirthday() const { return birthday; }
    virtual bool isGreater(const date& d) const {
        return birthday > d;
    }


    virtual const char* getFam() const = 0;
    virtual const char* getName() const = 0;
    virtual int getGrup() const { return 0; }
    virtual const char* getCourse() const { return ""; }
};

class Learner : public Common {
private:
    int grup;
    static int count;
public:
    Learner(const char* fam, const char* name, int d, int m, int y, int grup)
        : Common(fam, name, d, m, y), grup(grup) {
        count++;
    }
    ~Learner() { count--; }

    void print(ostream& os) const override {
        os << fio.getFam() << "\t" << fio.getName() << "\t" << grup << "\t" << birthday;
    }


    const char* getFam() const override { return fio.getFam(); }
    const char* getName() const override { return fio.getName(); }
    int getGrup() const override { return grup; }

    static int getCount() { return count; }
};
int Learner::count = 0;

class Tutor : public Common {
private:
    char* course;
    static int count;
public:
    Tutor(const char* fam, const char* name, int d, int m, int y, const char* course)
        : Common(fam, name, d, m, y) {
        this->course = new char[strlen(course) + 1];
        strcpy(this->course, course);
        count++;
    }
    ~Tutor() {
        delete[] course;
        count--;
    }

    void print(ostream& os) const override {
        os << fio.getFam() << "\t" << fio.getName() << "\t" << course << "\t" << birthday;
    }


    const char* getFam() const override { return fio.getFam(); }
    const char* getName() const override { return fio.getName(); }
    const char* getCourse() const override { return course; }

    static int getCount() { return count; }
};
int Tutor::count = 0;



class CommonList {
private:
    Common** items;
    int n;
public:
    CommonList() : items(nullptr), n(0) {}
    ~CommonList() {
        for (int i = 0; i < n; ++i) delete items[i];
        delete[] items;
    }

    void addItem(Common* item) {
        Common** newItems = new Common * [n + 1];
        for (int i = 0; i < n; ++i) newItems[i] = items[i];
        newItems[n] = item;
        delete[] items;
        items = newItems;
        n++;
    }

    void printAll() const {
        cout << "Фамилия\tИмя\tГр|Ку\tДата рождения\n";
        for (int i = 0; i < n; i++) {
            cout << *items[i] << endl;
        }
    }

    Common** findByDate(const date& targetDate, int* count) {
        *count = 0;
        for (int i = 0; i < n; ++i) {
            if (items[i]->isGreater(targetDate)) (*count)++;
        }

        if (*count == 0) return nullptr;

        Common** result = new Common * [*count];
        int idx = 0;
        for (int i = 0; i < n; ++i) {
            if (items[i]->isGreater(targetDate)) {
                result[idx++] = items[i];
            }
        }
        return result;
    }


    void saveToFile(const char* filename) {
        ofstream file(filename);
        if (!file.is_open()) {
            cerr << "Ошибка открытия файла для записи!" << endl;
            return;
        }
        for (int i = 0; i < n; ++i) {
            if (Learner* l = dynamic_cast<Learner*>(items[i])) {
                file << "L " << l->getFam() << " " << l->getName() << " "
                    << l->getBirthday().getDay() << " "
                    << l->getBirthday().getMonth() << " "
                    << l->getBirthday().getYear() << " "
                    << l->getGrup() << endl;
            }
            else if (Tutor* t = dynamic_cast<Tutor*>(items[i])) {
                file << "T " << t->getFam() << " " << t->getName() << " "
                    << t->getBirthday().getDay() << " "
                    << t->getBirthday().getMonth() << " "
                    << t->getBirthday().getYear() << " "
                    << t->getCourse() << endl;
            }
        }
        file.close();
    }

    void loadFromFile(const char* filename) {
        ifstream file(filename);
        if (!file.is_open()) {
            cerr << "Ошибка открытия файла для чтения!" << endl;
            return;
        }
        char type;
        while (file >> type) {
            string fam, name, course;
            int d, m, y, grup;
            if (type == 'L') {
                file >> fam >> name >> d >> m >> y >> grup;
                addItem(new Learner(fam.c_str(), name.c_str(), d, m, y, grup));
            }
            else if (type == 'T') {
                file >> fam >> name >> d >> m >> y >> course;
                addItem(new Tutor(fam.c_str(), name.c_str(), d, m, y, course.c_str()));
            }
        }
        file.close();
    }



};

int main() {
    setlocale(LC_ALL, "Russian");
    CommonList learners, tutors;
    int option;

    do {
        printf("\nМеню:\n");
        printf("1. Добавить студента\n");
        printf("2. Добавить преподавателя\n");
        printf("3. Найти по дате рождения\n");
        printf("4. Вывести всех\n");
        printf("5. Сохранить в файл\n");
        printf("6. Загрузить из файла\n");
        printf("7. Выход\n");
        printf("Выберите: ");
        scanf("%d", &option);

        char fam[256], name[256], course[256];
        int d, m, y, grup;
        date targetDate;
        Common** result;
        int count;

        switch (option) {
        case 1:
            printf("Фамилия: "); scanf("%s", fam);
            printf("Имя: "); scanf("%s", name);
            printf("Дата рождения (д м г): "); 
            scanf("%d %d %d", &d, &m, &y);
            while (d > 31, m > 12, y > 2006 || y < 1960 || (d > 28 && m == 2)) {
                printf("Такой даты быть не может, введите еще раз:\n");
                scanf("%d %d %d", &d, &m, &y);
            }
            printf("Группа: "); scanf("%d", &grup);
            learners.addItem(new Learner(fam, name, d, m, y, grup));
            break;
        case 2:
            printf("Фамилия: "); scanf("%s", fam);
            printf("Имя: "); scanf("%s", name);
            printf("Дата рождения (д м г): "); 
            scanf("%d %d %d", &d, &m, &y);
            while (d > 31, m > 12, y > 2006 || y < 1960 || (d > 28 && m == 2)) {
                printf("Такой даты быть не может, введите еще раз:\n");
                scanf("%d %d %d", &d, &m, &y);
            }
            printf("Курс: "); scanf("%s", course);
            tutors.addItem(new Tutor(fam, name, d, m, y, course));
            break;
        case 3:
            printf("Введите дату для поиска (д м г): "); 
            scanf("%d %d %d", &d, &m, &y);
            while (d > 31, m > 12, y > 2006 || y < 1960 || (d > 28 && m == 2)) {
                printf("Такой даты быть не может, введите еще раз:\n");
                scanf("%d %d %d", &d, &m, &y);
            }
            targetDate = date(d, m, y);
            result = learners.findByDate(targetDate, &count);
            if (result) {
                cout << "Студенты:\n";
                for (int i = 0; i < count; ++i) cout << *result[i] << endl;
                delete[] result;
            }
            result = tutors.findByDate(targetDate, &count);
            if (result) {
                cout << "Преподаватели:\n";
                for (int i = 0; i < count; ++i) cout << *result[i] << endl;
                delete[] result;
            }
            break;
        case 4:
            cout << "Студенты:\n";
            learners.printAll();
            cout << "Преподаватели:\n";
            tutors.printAll();
            break;
        case 5: {
            learners.saveToFile("learners.txt");
            tutors.saveToFile("tutors.txt");
            break;
        }
        case 6: {
            learners.loadFromFile("learners.txt");
            tutors.loadFromFile("tutors.txt");
            break;
        }
        case 7:
            printf("Выход\n");
            break;
        default:
            printf("Неверный выбор\n");
        }
    } while (option != 7);
    return 0;
}
