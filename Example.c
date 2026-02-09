#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>

const char names_m[7][15] = { "Андрей","Сергей", "Алексей", "Вадим", "Влад",
							"Слава", "Дима" };
const char names_f[7][15] = { "Кристина","Марина", "Елена", "Вера", "Надежда",
							"Любовь", "Валерия" };
const char surnames[8][15] = { "Спирин","Иванов", "Петров", "Сорокин",
							"Сидоров", "Соколов", "Сачков", "Брекоткин" };

typedef enum
{
	MALE,
	FEMALE
} Gender;

typedef struct
{
	int day;
	int month;
	int year;
} Date;

typedef struct
{
	char* name;
	char* surname;
	char* direction;
	int num_group;
} Student;

void init(Student*, int);
void print(Student*, int);
Student* AddStud();
Student* toGift(Student*, int, int*);

void writeFile(char[], Student*, int);
Student* readFile(char[], int*);
void addFile(char[], Student*, int);

int main()
{
	int n = 0;
	Student* studs = (Student*)malloc(n * sizeof(Student)), *studToGift, *studsFromFile = NULL;
	system("chcp 1251");
	char** str;
	str = (char**)malloc(sizeof(char*));
	char* buff = { ' ' };
	buff = (char*)malloc(sizeof(char));
	printf("Введите студента, его направление и группу, окончание ввода - пустая строка\n");
	do {
		gets(buff);
		if (*buff == ' ') {
			printf("Не стоит начинать с пробела, попробуйте с букв! :)\n");
		}
		else {
			n += 1;
			Add_Stud(studs, n);
		}
	} while (strcmp(buff, "\0"));
	/*Student petr = { "Petr", "Petrov", 2.3, 0, {29, 2, 2012} };
	printf("%s %s %.1lf %d-%d-%d\n", petr.name, petr.surname, petr.mark,
		petr.date.day, petr.date.month, petr.date.year);
	Student ivan = { .date = {3, 12, 2010}, .name = "Ivan",
		.surname = "Ivanov", .mark = 4.5, .gender = MALE };
	printf("%s %s %.1lf %d-%d-%d\n", ivan.name, ivan.surname, ivan.mark,
		ivan.date.day, ivan.date.month, ivan.date.year);*/
	
	print(studs, n);
	
	free(studs);
}


Student* Add_Stud(Student* arr) 
{

	arr->name = scanf("%s", name);
	arr->surname = scanf("%s", surname);
	arr->direction = scanf("%s", direction);
	arr->num_group = scanf("%s", num_group);
}

void init(Student* arr, int n)
{
	int i;
	char names[]
	void arr[];
	scanf("%s", &names);
	/*for (i = 0; i < n; i++)
	{
		strcpy(arr[i].name, names_m[rand() % 7]);
		strcpy(arr[i].surname, surnames[rand() % 8]);
		arr[i].mark = (rand() % 401 + 100) / 100.;
		arr[i].date.day = rand() % 28 + 1;
		arr[i].date.month = rand() % 12 + 1;
		arr[i].date.year = rand() % 5 + 2008;
	}*/
}

void print(Student* arr, int n)
{
	int i;
	for (i = 0; i < n; i++)
	{
		printf("%10s %10s %.2lf %2d-%2d-%4d\n", arr[i].name, arr[i].surname,
			arr[i].mark, arr[i].date.day, arr[i].date.month, arr[i].date.year);
	}
}

Student* toGift(Student* arr, int n, int* m)
{
	int i;
	*m = 0;
	for (i = 0; i < n; i++)
		if (arr[i].mark > 3.5)
			(*m)++;
	Student* res = (Student*)malloc((*m) * sizeof(Student));
	*m = 0;
	for (i = 0; i < n; i++)
		if (arr[i].mark > 3.5)
		{
			res[*m] = arr[i];
			(*m)++;
		}
	return res;
}

void writeFile(char filename[], Student* arr, int n)
{
	int i;
	FILE* fp = fopen(filename, "w");
	if (fp)
	{
		for (i = 0; i < n; i++)
			fprintf(fp, "%15s %15s %lf %d %d %d %d\n", arr[i].name, arr[i].surname,
				arr[i].mark, arr[i].gender, arr[i].date.day, arr[i].date.month,
				arr[i].date.year);
		fclose(fp);
	}
	else
		perror("Some problems with file: ");
}

Student* readFile(char filename[], int* n)
{
	Student temp = {""}, * res = NULL;
	int i;
	FILE* fp = fopen(filename, "r");
	if (fp)
	{
		*n = 0;
		while (fscanf(fp, "%15s %15s %lf %d %d %d %d\n", temp.name, temp.surname,
			&temp.mark, &temp.gender, &temp.date.day, &temp.date.month,
			&temp.date.year) != -1)
			(*n)++;
		res = (Student*)malloc((*n) * sizeof(Student));
		//fseek(fp, 0, SEEK_SET);
		rewind(fp);
		for (i = 0; i < *n; i++)
			fscanf(fp, "%15s %15s %lf %d %d %d %d\n", res[i].name, res[i].surname,
				&res[i].mark, &res[i].gender, &res[i].date.day, &res[i].date.month,
				&res[i].date.year);
		fclose(fp);
	}
	else perror("Some problems with file: ");
	
	return res;
}

void addFile(char filename[], Student* arr, int n)
{
	int i, m;
	FILE* fp = fopen(filename, "a+");
	if (fp)
	{
		for (i = 0; i < n; i++)
			fprintf(fp, "%15s %15s %lf %d %d %d %d\n", arr[i].name, arr[i].surname,
				arr[i].mark, arr[i].gender, arr[i].date.day, arr[i].date.month,
				arr[i].date.year);
		//rewind(fp);
		//fscanf(fp, "%d", &m);
		//rewind(fp);
		//fprintf("%d\n", m + n);
		fclose(fp);
	}
	else
		perror("Some problems with file: ");
}
