#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// 24

typedef struct {
    char* fam; 		//фамилия студента
    char* name;		// имя студента
    int group;		// номер группы

} Student;

void Add_Stud(Student* arr, int i);
void print(Student* arr, int n);
void find_by_fio(Student* arr, int n);
void find_by_group(Student* arr, int n);
void writeFile(char filename[], Student* arr, int n);
Student* readFile(char filename[], int n);

int main()
{
    system("chcp 1251");
    int cap = 2;
    int n = 0;
    Student* studs = (Student*)malloc(cap * sizeof(Student));
    printf("Чтобы добавить студента введите, группу, фамилию и имя.\n");
    if (access("stud", 0) != 0) {
        printf("Чтобы добавить студента, его направление и группу введите 'Добавить', окончание ввода - слово 'Все'\n");
        char* buff;
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
        writeFile("stud", studs, n);
    }
    else {
        studs = readFile("stud", n);
    }
    // Вывод студентов
    print(studs, n);
    int search;
    printf("Выберите тип поиска\nПо фамилии и имени - 1\nПо группе - 2\nЧтобы ничего не искать введите что-то кроме 1 и 2\n");
    scanf("%d", &search);
    getchar();
    switch (search)
    {
    case 1:
        find_by_fio(studs, n);
        break;
    case 2:
        find_by_group(studs, n);
        break;
    default:
        break;
    }
}


void find_by_fio(Student* arr, int n) {
    int group;
    char* s_fam;
    s_fam = (char*)malloc(sizeof(50));
    printf("Введите фамилию необходимого студента: ");
    gets(s_fam);
    getchar();
    char* s_name;
    s_name = (char*)malloc(sizeof(50));
    printf("Введите имя необходимого студента: ");
    gets(s_name);
    printf("%s %s\n", s_fam, s_name);
    for (int i = 0; i < n; i++)
    {
        if (!strcmp(arr[i].name, s_name) && !strcmp(arr[i].fam, s_fam))
        {

            printf("Номер группы: %d\n", arr[i].group);
        }
    }
}

void print(Student* arr, int n)
{
    printf("\nСписок студентов:\n");
    for (int i = 0; i < n; i++)
    {
        printf("%s %s %d\n", arr[i].name, arr[i].fam, arr[i].group);
    }
}


void find_by_group(Student* arr, int n) {
    int group;
    printf("Введите номер необходимой группы: ");
    scanf("%d", &group);
    printf("Студенты %d группы:\n", group);
    for (int i = 0; i < n; i++)
    {
        if (arr[i].group == group)
        {
            printf("%d. %s %s\n", i + 1, arr[i].fam, arr[i].name);
        }
    }
}


void Add_Stud(Student* arr, int i)
{
    arr[i].fam = (char*)malloc(100 * sizeof(char));
    arr[i].name = (char*)malloc(100 * sizeof(char));
    int gr;
    printf("\nВведите имя: ");
    gets(arr[i].name, 100, stdin);

    printf("Введите фамилию: ");
    gets(arr[i].fam, 100, stdin);

    printf("Введите группу: ");
    scanf("%d", &gr);
    arr[i].group = gr;
    getchar();
}

void writeFile(char filename[], Student* arr, int n)
{
    int i;
    FILE* fp = fopen(filename, "w");
    if (fp)
    {
        for (i = 0; i < n; i++)
            fprintf(fp, "%s %s %d\n", arr[i].name, arr[i].fam,
                arr[i].group);
        fclose(fp);
        printf("Файл записан\n");
    }
    else
        perror("Some problems with file: ");
}

Student* readFile(char filename[], int n)
{
    Student temp = { "" }, *res = NULL;
    temp.name = (char*)malloc(50);
    temp.fam = (char*)malloc(50);
    int i;
    FILE* fp = fopen(filename, "r");
    if (fp)
    {
        n = 0;
        while (fscanf(fp, "%s %s %d\n", temp.name, temp.fam,
            &temp.group) != -1)
            n++;
        res = (Student*)malloc(n * sizeof(Student));
        for (i = 0; i < n; i++)
            (fp, "%s %s %d\n", temp.name, temp.fam,
                &temp.group);
        fseek(fp, 0, SEEK_SET);
        rewind(fp);
        fclose(fp);
    }
    else perror("Some problems with file: ");

    return res;
}
