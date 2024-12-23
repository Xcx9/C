#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct
{
    char* name;
    char* surname;
} Fio;

typedef struct
{
    int day;
    char* month;
    int year;
} Date;

typedef struct
{
    Fio fio;
    Date date;
    char* direction;
    int group;
} Student;

void Add_Stud(Student* arr, int i);
void print(Student* arr, int n);
void find_by_direction(Student* arr, int n, char* direction);
void find_by_group(Student* arr, int n, char* direction, int group);
void writeFile(char filename[], Student* arr, int n);
Student* readFile(char filename[], int* n);

int main()
{
    system("chcp 1251");
    int n = 0;
    int cap = 2;
    Student* studs = (Student*)malloc(cap * sizeof(Student));
    if (access("stud", 0) != 0) {
        printf("Чтобы добавить студента, его направление и группу введите 'Добавить', окончание ввода - слово 'Все'\n");
        char* buff = { ' ' };
        buff = (char*)malloc(sizeof(char));

        while (1) {
            printf("Введите что-нибудь для добавления студента или 'все' для завершения: ");
            gets(buff);

            if (!strcmp(buff, "все")) {
                break;
            }

            if (n >= cap) {
                cap *= 2;
                studs = (Student*)realloc(studs, cap * sizeof(Student));
            }
            Add_Stud(studs, n);
            n++;
        }
    }
    else {
        studs = readFile("stud", n);
    }


    // Вывод студентов
    print(studs, n);
    writeFile("stud", studs, n);
    int search;
    printf("Выберите тип поиска\nПо направлению - 1\nПо группе и направлению - 2\nЧтобы ничего не искать введите что-то кроме 1 и 2\n");
    scanf("%d", &search);

    char* direction;
    direction = (char*)malloc(100 * sizeof(char));

    printf("\nВведите направление подготовки для поиска: ");
    scanf("%s", direction);

    switch (search)
    {
    case 1:
        // Поиск по направлению
        find_by_direction(studs, n, direction);
        break;
    case 2:
        // Поиск по группе и направлению
        printf("\nВведите номер группы: ");
        int group;
        scanf("%d", &group);
        find_by_group(studs, n, direction, group);
        break;
    default:
        break;
    }
    writeFile("studs", studs, n);
    for (int i = 0; i < n; i++) {
        free(studs[i].fio.name);
        free(studs[i].fio.surname);
        free(studs[i].direction);
    }
    free(studs);

    return 0;
}

void Add_Stud(Student* arr, int i)
{
    arr[i].fio.name = (char*)malloc(100 * sizeof(char));
    arr[i].fio.surname = (char*)malloc(100 * sizeof(char));
    arr[i].direction = (char*)malloc(100 * sizeof(char));

    printf("\nВведите имя: ");
    gets(arr[i].fio.name);
    printf("Введите фамилию: ");
    gets(arr[i].fio.surname);
    printf("Введите направление: ");
    gets(arr[i].direction);
    printf("Введите номер группы: ");
    scanf("%d", &arr[i].group);
    getchar();
}

void print(Student* arr, int n)
{
    printf("\nСписок студентов:\n");
    for (int i = 0; i < n; i++)
    {
        printf("%s %s %s %d\n", arr[i].fio.name, arr[i].fio.surname, arr[i].direction, arr[i].group);
    }
}

void find_by_direction(Student* arr, int n, char* direction)
{
    printf("\nСтуденты по направлению '%s':\n", direction);
    for (int i = 0; i < n; i++)
    {
        if (!strcmp(arr[i].direction, direction))
        {
            printf("Имя: %s Фамилия: %s Группа: %d\n", arr[i].fio.name, arr[i].fio.surname, arr[i].group);
        }
    }
}

void find_by_group(Student* arr, int n, char* direction, int group)
{
    Student* new_arr = (Student*)malloc(n * sizeof(Student));
    int cnt = 0;

    for (int i = 0; i < n; i++) {
        if (!strcmp(arr[i].direction, direction) && arr[i].group == group) {
            new_arr[cnt++] = arr[i];
        }
    }

    for (int i = 0; i < cnt - 1; i++) {
        for (int j = 0; j < cnt - i - 1; j++) {
            if (strcmp(new_arr[j].fio.name, new_arr[j + 1].fio.name)) {
                Student temp = new_arr[j];
                new_arr[j] = new_arr[j + 1];
                new_arr[j + 1] = temp;
            }
        }
    }

    printf("\nСтуденты группы '%s %d':\n", direction, group);
    for (int i = 0; i < cnt; i++) {
        printf("%d) %s %s\n", i + 1, new_arr[i].fio.name, new_arr[i].fio.surname);
    }

    free(new_arr);
}

void writeFile(char filename[], Student* arr, int n)
{
    int i;
    FILE* fp = fopen(filename, "w");
    if (fp)
    {
        for (i = 0; i < n; i++)
            fprintf(fp, "%s %s %d %s %d %s %d\n", arr[i].fio.name, arr[i].fio.surname,
                arr[i].date.day, arr[i].date.month,
                arr[i].date.year, arr[i].direction, arr[i].group);
        fclose(fp);
    }
    else
        perror("Some problems with file: ");
}

Student* readFile(char filename[], int n)
{
    Student temp = { "" }, * res = NULL;
    int i;
    FILE* fp = fopen(filename, "r");
    if (fp)
    {
        n = 0;
        while (fscanf(fp, "%s %s(%d-%d-%d): %s %d  \n", temp.fio.name, temp.fio.surname,
            &temp.date.day, &temp.date.month, &temp.date.year, &temp.direction, &temp.group) != -1);
            n++;
        res = (Student*)malloc(n * sizeof(Student));
        fseek(fp, 0, SEEK_SET);
        rewind(fp);
        for (i = 0; i < n; i++)
            fscanf(fp, "%s %s(%d-%d-%d): %s %d  \n", temp.fio.name, temp.fio.surname,
                &temp.date.day, &temp.date.month, &temp.date.year, &temp.direction, &temp.group);
        fclose(fp);
    }
    else perror("Some problems with file: ");

    return res;
}
