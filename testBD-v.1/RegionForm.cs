using System;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using testBD_v._1.ModelDB;
using System.Linq;
using System.Data;


namespace testBD_v._1
{
    public partial class RegionForm : Form
    {
        // Инициализируем объект нашего контекста
        ModelKeyBTcontext db;

        // Инициализируем форму
        public RegionForm()
        {
            InitializeComponent();
        }

        //Загрузка данных
        private void RegionForm_Load(object sender, EventArgs e)
        {   
            // Открываем и закрываем соединение с базой данных с использованием очисти памяти
            using (db)
            {
                // Создаём объект нашего контекста
                db = new ModelKeyBTcontext();
                // Загружаем данные из таблицы в кэш
                db.Справочник_районы.Load();
                // Привязываем данные к dataGridView
                dataGridView1.DataSource = db.Справочник_районы.Local.ToBindingList();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[0].Width = 70;
            }
        }

        //Строка поиска
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Справочник_районы.Where(x => x.Код_района.ToString().Contains(txtSearch.Text)
              || x.Район.Contains(txtSearch.Text)).ToList();
        }

        //Добавить район
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Проверяем, что в текстовых полях есть данные
            if (txtRaion.Text == String.Empty)
            {
                MessageBox.Show("Заполните данные о районе!");
                return;
            }

            // Создаём экземпляр класса Справочник_районы
            Справочник_районы raion = new Справочник_районы();
            {
                raion.Район = txtRaion.Text;
            }

            // Заносим данные в нашу таблицу
            db.Справочник_районы.Add(raion);
            // Обязательно сохраняем изменения
            db.SaveChanges();
            // Обновляем наш dataGridView
            dataGridView1.Refresh();
            // Обнуляем текстовые поля
            Clear();
        }

        //Редактировать район
        private void bntEdit_Click(object sender, EventArgs e)
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
            Справочник_районы raion = db.Справочник_районы.Find(id);
            if (raion == null) return;
            raion.Район = txtRaion.Text;
            // Добавляем или обновляем запись
            db.Справочник_районы.AddOrUpdate(raion);
            // Сохранаям данные в базе данных
            db.SaveChanges();
            // Обновляем таблицу с данными
            dataGridView1.Refresh();
        }

        //Удалить район
        private void btnRemove_Click(object sender, EventArgs e)
        {   
            //Проверяем что запись выбрана
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
                Справочник_районы raion = db.Справочник_районы.Find(id);
                if (raion == null) return;
                raion.Район = txtRaion.Text;
                // Удаляем запись из базы данных
                db.Справочник_районы.Remove(raion);
                // Сохраняем запись в базы данных
                db.SaveChanges();
                // Обновляем таблицу с данными
                dataGridView1.Refresh();
                // Очищаем поля
                Clear();
            }
        }

        // Событие: клик по ячейке таблицы
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверка выборки строк
            // Если строка не выбрана, то дальше ничего не происходит
            if (dataGridView1.CurrentRow == null) return;

            // Получаем выделенную строку и приводим её у типу Countries
            Справочник_районы raion = dataGridView1.CurrentRow.DataBoundItem as Справочник_районы;

            // Если мы щёлкаем по пустой строке, то ничего дальше не делаем
            if (raion == null) return;

            // Выводим данные о стране и её столице в TextBox
            txtRaion.Text = raion.Район;
            txtID.Text = raion.Код_района.ToString();
        }

        // Обнуляем текстовые поля
        void Clear()
        {
            txtID.Text = String.Empty;
            txtRaion.Text = String.Empty;
            txtSearch.Text = String.Empty;
        }

        // Очистить поля
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
