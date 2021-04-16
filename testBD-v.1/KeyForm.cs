using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using testBD_v._1.ModelDB;
using System.Data.Entity.Migrations;

namespace testBD_v._1
{
    public partial class KeyForm : Form
    {
        // Инициализируем объект нашего контекста
        ModelKeyBTcontext db;

        // Инициализация формы
        public KeyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка данных из базы данных
        /// </summary>
        public void refresh()
        {
            using (db)
            {
                // Создаём объект нашего контекста
                db = new ModelKeyBTcontext();
                // Загружаем данные из таблицы в кэш
                db.Справочник_ключи.Load();

                // Привязываем данные к dataGridView
                dataGridView1.DataSource = db.Справочник_ключи.Local.ToBindingList();

                dataGridView1.Columns[0].Width = 40;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[dataGridView1.ColumnCount - 1].Visible = false;
                dataGridView1.Columns[dataGridView1.ColumnCount - 2].Visible = false;
                dataGridView1.Columns["Код_района"].Visible = false;
                dataGridView1.Columns["Код_расположения_АВ"].Visible = false;
                // Привязываем данные
                cbRegion.DataSource = db.Справочник_районы.ToList();
                cbLocation.DataSource = db.Справочник_расоложение_АВ.ToList();
                txtID.Enabled = false;
            }
        }

        // Загрузка данных из базы данных
        private void KeyForm_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Проверяем, что в текстовых полях есть данные
            if ((txtPod.Text == String.Empty) || (txtAdr.Text == "") || (cbRegion.SelectedValue == null) || (cbLocation.SelectedValue == null))
            {
                MessageBox.Show("Заполните данные");
                return;
            }

            //  Создаём экземпляр класса Справочник_ключи,
            // т.е получаем данные о нашем узле связяи из текстовых полей
            Справочник_ключи keys = new Справочник_ключи();
            {
                keys.Адрес = txtAdr.Text;
                keys.Подъезд = Convert.ToInt32(txtPod.Text);
                keys.Код_района = ((Справочник_районы)cbRegion.SelectedItem).Код_района;
                keys.Код_расположения_АВ = ((Справочник_расоложение_АВ)cbLocation.SelectedItem).Код_расположения_АВ;
            }

            // Заносим данные в нашу таблицу
            db.Справочник_ключи.Add(keys);
            // Обязательно сохраняем изменения
            db.SaveChanges();
            // Обновляем наш dataGridView, чтобы в нём отобразилась новая страна
            dataGridView1.Refresh();
            // Обнуляем текстовые поля
            Clear();
        }

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
            // Находим ключ по этому id с помощью метода Find()
            Справочник_ключи keys = db.Справочник_ключи.Find(id);
            if (keys == null) return;
            keys.Адрес = txtAdr.Text;
            keys.Подъезд = Convert.ToInt32(txtPod.Text);
            keys.Код_района = ((Справочник_районы)cbRegion.SelectedItem).Код_района;
            keys.Код_расположения_АВ = ((Справочник_расоложение_АВ)cbLocation.SelectedItem).Код_расположения_АВ;
            // Добавляем или обновляем запись
            db.Справочник_ключи.AddOrUpdate(keys);
            // Сохраняем запись в базы данных
            db.SaveChanges();
            // Обновляем таблицу с данными
            dataGridView1.Refresh();
        }

        // Удалить запись
        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Проверяем, что выбрана запись
            if (txtID.Text == String.Empty)
            {
                MessageBox.Show("Не выбрана запись!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Удалить запись?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Получаем id из текстового поля
                int id = Convert.ToInt32(txtID.Text);
                // Находим ключ по этому id с помощью метода Find()
                Справочник_ключи keys = db.Справочник_ключи.Find(id);
                // Удаляем запись в базе данных
                db.Справочник_ключи.Remove(keys);
                // сохраняем базу данных
                db.SaveChanges();
                // Обновляем таблицу с данными
                dataGridView1.Refresh();
                // Очищяем поля
                Clear();
            }
        }

        //Если выбрана строка
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверка выборки строк
            // Если строка не выбрана, то дальше ничего не происходит
            if (dataGridView1.CurrentRow == null) return;

            // Получаем выделенную строку и приводим её у типу Countries
            Справочник_ключи location = dataGridView1.CurrentRow.DataBoundItem as Справочник_ключи;

            // Если мы щёлкаем по пустой строке, то ничего дальше не делаем
            if (location == null) return;

            // Выводим данные 
            txtID.Text = location.Код_ключа.ToString();
            txtAdr.Text = location.Адрес;
            txtPod.Text = location.Подъезд.ToString();
            cbLocation.SelectedItem = location.Справочник_расоложение_АВ;
            cbRegion.SelectedItem = location.Справочник_районы;
        }

        /// <summary>
        /// Очистка полей
        /// </summary>
        void Clear()
        {
            txtID.Text = "";
            txtAdr.Text = "";
            txtPod.Text = "";
            cbLocation.SelectedItem = null;
            cbRegion.SelectedItem = null;
        }

        // Очищаем поля
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        // Обновляем таблицу с данными
        private void button2_Click(object sender, EventArgs e)
        {
            refresh();
        }

        //Строка поиска
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            db = new ModelKeyBTcontext();
            // Загружаем данные из таблицы в кэш
            db.Справочник_ключи.Load();
            // Привязываем данные к dataGridView
            BindingList<Справочник_ключи> list = db.Справочник_ключи.Local.ToBindingList();

            dataGridView1.DataSource = list;
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.RowHeadersVisible = false;

            // Запрос поиска 
            dataGridView1.DataSource = db.Справочник_ключи.Where(x => x.Код_ключа.ToString().Contains(tbSearch.Text)
                || x.Адрес.Contains(tbSearch.Text)).ToList();
        }
    }
}

