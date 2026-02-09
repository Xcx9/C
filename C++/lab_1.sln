#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
using namespace std;

class Student {
private:
	char* surname;
	char* name;
	char** stud;
public:
	int group;
	int static count;

	Student()
	{
		count++;
		cout << "\nЧто-то не было введено, попробуйте еще раз\n";
	}

	Student(char* name, char*surname, int group)
	{
		count++;
		surname = this->surname;
		name = this->name;
		group = this->group;
	};

	~Student()
	{
		free(name);
		free(surname);
	};

	void static printStatic();

	void add_stud()
	{
		int decision;
		name = (char*)malloc(100 * sizeof(char));
		surname = (char*)malloc(100 * sizeof(char));

		cout << "Хотите добавить студентов?(1 - Да; 2 - Нет)\n";
		cin >> decision;
		int count = 0;
		do {
			cout << "Введите фамилию студента: ";
			cin >> surname;
			cout << "Введите имя студента: ";
			cin >> name;
			cout << "Введите группу студента: ";
			cin >> group;
		/*	stud[count][0] = name; 
			stud[count][1] = surname;
			stud[count][2] = group;*/
			cout << "Хотите добавить студентов?(1 - Да; 2 - Нет)\n";
			cin >> decision;
		} while (decision == 1);
	}

	void print_stud(Student* arr, int n)
	{
		printf("\nСписок студентов:\n===============================\n");
		for (int i = 0; i < n; i++)
		{
			printf("%s %s: %s\n", arr[i].name, arr[i].surname,
				arr[i].group);
		}
		printf("===============================\n");
	}

};


int Student::count = 0;


int main() {
	
	
	system("chcp 1251");
	Student::printStatic();
	Student temp;
	Student::printStatic();
	temp.add_stud();
	//temp.print_stud();
}


void Student::printStatic()
{
	std::cout << "====================\n" << count << std::endl << "====================\n";
}
