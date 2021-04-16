using System;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using testBD_v._1.ModelDB;
using System.Linq;
using System.Data;
using System.ComponentModel;

namespace testBD_v._1
{

    public partial class UserForm : Form 
    {
        // Инициализируем объект нашего контекста
        ModelKeyBTcontext db;
        
        // Инициализируем форму
        public UserForm()
        {
            InitializeComponent();
        }

        // Событие: клик по ячейке таблицы
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверка выборки строк
            // Если строка не выбрана, то дальше ничего не происходит
            if (dgvUsers.CurrentRow == null) return;

            // Получаем выделенную строку и приводим её у типу Countries
            Справочник_сотрдники user = dgvUsers.CurrentRow.DataBoundItem as Справочник_сотрдники;

            // Если мы щёлкаем по пустой строке, то ничего дальше не делаем
            if (user == null) return;

            // Выводим данные о стране и её столице в TextBox
            txtName.Text = user.ФИО;
            txtPhone.Text = user.Телефон;
            txtID.Text = user.Код_сотрудника.ToString();
        }

        //Добавление сотрудников
        private void btnAddUser_Click_1(object sender, EventArgs e)
        {
            // Проверяем, что в текстовых полях есть данные
            if (txtName.Text == String.Empty || txtPhone.Text == String.Empty)
            {
                MessageBox.Show("Заполните данные о сотруднике!");
                return;
            }

            // Создаём экземпляр класса Справочник_сотрудники,
            // т.е получаем данные о нашем сотруднике из текстовых полей
            Справочник_сотрдники user = new Справочник_сотрдники();
            {
                user.ФИО = txtName.Text;
                user.Телефон = txtPhone.Text;
            }

            // Заносим данные в нашу таблицу
            db.Справочник_сотрдники.Add(user);
            // Обязательно сохраняем изменения
            db.SaveChanges();
            // Обновляем наш dataGridView, чтобы в нём отобразилась новая страна
            dgvUsers.Refresh();
            // Обнуляем текстовые поля
            Clear();
        }

        //Редактирование записи о сотруднике
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            // Проверяем, что выбрана запись
            if (txtID.Text == String.Empty)
            {
                MessageBox.Show("Не выбрана запись!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Получаем id из текстового поля
            int id = Convert.ToInt32(txtID.Text);
            // Находим запись по этому id с помощью метода Find()
            Справочник_сотрдники user = db.Справочник_сотрдники.Find(id);
            if (user == null) return;
            user.ФИО = txtName.Text;
            user.Телефон = txtPhone.Text;
            // Добавляем или обновляем запись
            db.Справочник_сотрдники.AddOrUpdate(user);
            // Сохраняем в бузу данных
            db.SaveChanges();
            // Обновляем таблицу с данными
            dgvUsers.Refresh();
        }

        //Очистить поля формы
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        //Удаление записи о сотруднике
        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            // Проверяем, что выбрана запись
            if (txtID.Text == String.Empty)
            {
                MessageBox.Show("Не выбрана запись!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //
            if (MessageBox.Show("Удалить запись?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Получаем id из текстового поля
                int id = Convert.ToInt32(txtID.Text);
                // Находим запись по этому id с помощью метода Find()
                Справочник_сотрдники user = db.Справочник_сотрдники.Find(id);
                if (user == null) return;
                //Если запись выбрана, вносим значения в поля
                user.ФИО = txtName.Text;
                user.Телефон = txtPhone.Text;
                // Удаляем запись из базы данных
                db.Справочник_сотрдники.Remove(user);
                // Сохраняем в бузу данных
                db.SaveChanges();
                // Обновляем таблицу с данными
                dgvUsers.Refresh();
                Clear();
            }
        }
        
        /// <summary>
        /// Очистка текстовых блоков
        /// </summary>
        void Clear()
        {
            // Обнуляем текстовые поля
            txtID.Text = String.Empty;
            txtName.Text = String.Empty;
            txtPhone.Text = String.Empty;
            txtSearch.Text = String.Empty;
        }

        /// <summary>
        /// Загрузка 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserForm_Load(object sender, EventArgs e)
        {
            // Создаём объект нашего контекста
            db = new ModelKeyBTcontext();
            // Загружаем данные из таблицы в кэш
            db.Справочник_сотрдники.Load();
            // Привязываем данные к dataGridView
            dgvUsers.DataSource = db.Справочник_сотрдники.Local.ToBindingList();
            
            dgvUsers.Columns[0].Width = 70;
            dgvUsers.Columns[1].Width = 340;
            dgvUsers.RowHeadersVisible = false;
        }

        
        //Строка поиска
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            db = new ModelKeyBTcontext();
            // Загружаем данные из таблицы в кэш
            db.Справочник_сотрдники.Load();
            // Привязываем данные к dataGridView
            BindingList<Справочник_сотрдники> list = db.Справочник_сотрдники.Local.ToBindingList();

            dgvUsers.DataSource = list;
            dgvUsers.Columns[0].Width = 70;
            dgvUsers.Columns[1].Width = 340;
            dgvUsers.RowHeadersVisible = false;

            // Запрос для поисковой строки поиск по коду сотрудника, ФИО, телефону
            dgvUsers.DataSource = db.Справочник_сотрдники.Where(x => x.Код_сотрудника.ToString().Contains(txtSearch.Text)
               || x.ФИО.ToString().Contains(txtSearch.Text)
               || x.Телефон.Contains(txtSearch.Text)).ToList();
        }
    }
}
