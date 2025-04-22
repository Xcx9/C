#include <iostream>
#include <cstdlib>
#include <ctime>

using namespace std;

// Шаблонная функция инициализации массива
template <typename T>
void initArray(T* arr, int size) {
    for (int i = 0; i < size; ++i) {
        arr[i] = rand() % 100; // Простые числа от 0 до 99
    }
}

// Явная перегрузка для char
template <>
void initArray<char>(char* arr, int size) {
    for (int i = 0; i < size; ++i) {
        arr[i] = 'A' + (rand() % 26); // Только заглавные буквы A-Z
    }
}

// Создание массива индексов (значения <= K)
template <typename T>
int* getIndices(const T* arr, int size, T K, int& resultSize) {
    resultSize = 0;
    for (int i = 0; i < size; ++i) {
        if (arr[i] <= K) resultSize++;
    }

    int* indices = new int[resultSize];
    int idx = 0;
    for (int i = 0; i < size; ++i) {
        if (arr[i] <= K) indices[idx++] = i;
    }
    return indices;
}

// Шаблонный класс DynamicArray
template <typename T>
class DynamicArray {
private:
    T* data;
    int size;
public:
    DynamicArray(int n) : size(n) {
        data = new T[size];
        for (int i = 0; i < size; ++i) {
            data[i] = rand() % 201 - 100; // Диапазон [-100, 100]
        }
    }

    ~DynamicArray() {
        delete[] data;
    }

    void print() {
        for (int i = 0; i < size; ++i) {
            cout << data[i] << " ";
        }
        cout << endl;
    }

    T* findMaxPositive() {
        T maxVal = -1;
        T* maxPtr = nullptr;
        for (int i = 0; i < size; ++i) {
            if (data[i] > 0 && data[i] > maxVal) {
                maxVal = data[i];
                maxPtr = &data[i];
            }
        }
        return maxPtr;
    }
};

int main() {
    srand(time(nullptr));

    // Тестирование с double
    double arrDouble[5];
    initArray(arrDouble, 5);
    cout << "Double array: ";
    for (int i = 0; i < 5; ++i) cout << arrDouble[i] << " ";
    cout << endl;

    double K;
    cout << "Enter K: ";
    cin >> K;
    int count;
    int* indices = getIndices(arrDouble, 5, K, count);
    cout << "Indices (<= K): ";
    for (int i = 0; i < count; ++i) cout << indices[i] << " ";
    cout << endl;
    delete[] indices;

    // Тестирование с char
    char arrChar[5];
    initArray(arrChar, 5);
    cout << "Char array: ";
    for (int i = 0; i < 5; ++i) cout << arrChar[i] << " ";
    cout << endl;

    // Тестирование класса
    DynamicArray<int> da(8);
    cout << "Dynamic array: ";
    da.print();
    int* maxPos = da.findMaxPositive();
    if (maxPos) cout << "Max positive: " << *maxPos << endl;
    else cout << "No positive elements" << endl;

    return 0;
}
