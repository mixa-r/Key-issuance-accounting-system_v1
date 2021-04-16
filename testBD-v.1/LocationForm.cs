using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Forms;
using testBD_v._1.ModelDB;

namespace testBD_v._1
{
    public partial class LocationForm : Form
    {
        // Инициализируем объект нашего контекста
        ModelKeyBTcontext db;

        // Инициализация формы
        public LocationForm()
        {
            InitializeComponent();
        }

        //Загрузка данных
        private void LocationForm_Load(object sender, EventArgs e)
        {
            using (db)
            {
                // Создаём объект нашего контекста
                db = new ModelKeyBTcontext();
                // Загружаем данные из таблицы в кэш
                db.Справочник_расоложение_АВ.Load();
                // Привязываем данные к dataGridView
                dataGridView1.DataSource = db.Справочник_расоложение_АВ.Local.ToBindingList();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[0].Width = 70;
            }
        }

        //Строка поиска
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Справочник_расоложение_АВ.Where(x => x.Код_расположения_АВ.ToString().Contains(txtSearch.Text)
              || x.Расположение_АВ.Contains(txtSearch.Text)).ToList();
        }

        //Добавить запись
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Проверяем, что в текстовых полях есть данные
            if (txtName.Text == String.Empty)
            {
                MessageBox.Show("Заполните данные о районе!");
                return;
            }

            // Создаём экземпляр класса Справочник_расоложение_АВ,
            Справочник_расоложение_АВ location = new Справочник_расоложение_АВ();
            {
                location.Расположение_АВ = txtName.Text;
            }

            // Заносим данные в нашу таблицу
            db.Справочник_расоложение_АВ.Add(location);
            // Обязательно сохраняем изменения
            db.SaveChanges();
            // Обновляем наш dataGridView
            dataGridView1.Refresh();
            // Обнуляем текстовые поля
            Clear();
        }

        //Редактировать запись
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Проверяем, что выбрана запись
            if (txtID.Text == String.Empty)
            {
                MessageBox.Show("Не выбрана запись!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Получаем id из текстового поля
            int id = Convert.ToInt32(txtID.Text);
            // Находим страну по этому id с помощью метода Find()
            Справочник_расоложение_АВ location = db.Справочник_расоложение_АВ.Find(id);
            if (location == null) return;
            location.Расположение_АВ = txtName.Text;
            // Добавляем или обновляем запись
            db.Справочник_расоложение_АВ.AddOrUpdate(location);
            // Обязательно сохраняем изменения
            db.SaveChanges();
            // Обновляем наш dataGridView
            dataGridView1.Refresh();
        }

        //Удалить запись
        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Проверяем, что в текстовых полях есть данные
            if (txtID.Text == String.Empty)
            {
                MessageBox.Show("Не выбрана запись!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Если запись выбрана
            if (MessageBox.Show("Удалить запись?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Получаем id из текстового поля
                int id = Convert.ToInt32(txtID.Text);
                // Находим запись по этому id с помощью метода Find()
                Справочник_расоложение_АВ location = db.Справочник_расоложение_АВ.Find(id);
                if (location == null) return;
                location.Расположение_АВ = txtName.Text;
                // Удаляем запись из базы данных
                db.Справочник_расоложение_АВ.Remove(location);
                // Сохраняем базу данных
                db.SaveChanges();
                // Обновляем таблицу с данными
                dataGridView1.Refresh();
                // Очищаем поля
                Clear();
            }
        }

        //Очимстиь поля
        void Clear()
        {
            // Обнуляем текстовые поля
            txtID.Text = String.Empty;
            txtName.Text = String.Empty;
            txtSearch.Text = String.Empty;
        }

        //Действия при выборе ячейки
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверка выборки строк
            // Если строка не выбрана, то дальше ничего не происходит
            if (dataGridView1.CurrentRow == null) return;

            // Получаем выделенную строку и приводим её у типу Countries
            Справочник_расоложение_АВ location = dataGridView1.CurrentRow.DataBoundItem as Справочник_расоложение_АВ;

            // Если мы щёлкаем по пустой строке, то ничего дальше не делаем
            if (location == null) return;

            // Выводим данные о стране и её столице в TextBox
            txtName.Text = location.Расположение_АВ;
            txtID.Text = location.Код_расположения_АВ.ToString();
        }

        // Очистка текстовых полей 
        private void txtClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
