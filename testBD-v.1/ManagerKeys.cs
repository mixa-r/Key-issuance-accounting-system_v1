using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using testBD_v._1.ModelDB;
using System.Data.Entity;


namespace testBD_v._1
{
    public partial class ManagerKeys : Form
    {
        // Инициализируем объект нашего контекста
        ModelKeyBTcontext db;

        List<Журнал> addedData = new List<Журнал>();

        // Инициализация формы
        public ManagerKeys()
        {
            InitializeComponent();
        }

        // Добавляем записи в таблицу для выдачи ключей
        private void Add_Click(object sender, EventArgs e)
        {

            // Проверяем, что в текстовых полях есть данные
            if ((cbSotr.SelectedValue == null) || (cbKey.SelectedValue == null))
            {
                MessageBox.Show("Заполните данные");
                return;
            }

            // Создаём экземпляр класса Журнал, и вносим информацию по для журнала
            Журнал jour = new Журнал();
            {
                jour.Дата_выдачи = DateTime.Now;
                jour.Дата_возврата = DateTime.Now;
                jour.Информация = "";
                jour.Код_ключа = ((Справочник_ключи)cbKey.SelectedItem).Код_ключа;
                jour.Код_сотрудника = ((Справочник_сотрдники)cbSotr.SelectedItem).Код_сотрудника;
                jour.Справочник_ключи = ((Справочник_ключи)cbKey.SelectedItem);
                jour.Справочник_сотрдники = ((Справочник_сотрдники)cbSotr.SelectedItem);
                jour.Статус = false;
            }
                
                addedData.Add(jour);
                dgData.Rows.Add();
                dgData.Rows[dgData.RowCount - 1].Cells[0].Value = jour.Справочник_ключи.Код_ключа;
                dgData.Rows[dgData.RowCount - 1].Cells[1].Value = jour.Справочник_ключи.Адрес;
                dgData.Rows[dgData.RowCount - 1].Cells[2].Value = jour.Справочник_ключи.Расположение_АВ;
                dgData.Rows[dgData.RowCount - 1].Cells[3].Value = jour.Справочник_сотрдники.ФИО;
            
        }

        // Сохраняем и передаем записи в журнал для приема-выдачи ключей
        private void Save_Click(object sender, EventArgs e)
        {
            for(int i =0;i<addedData.Count();i++)
            {
                Журнал jour = new Журнал();
                {
                    jour.Дата_выдачи = DateTime.Now;
                    jour.Дата_возврата = DateTime.Now;
                    jour.Информация = "";
                    jour.Код_ключа = addedData[i].Код_ключа;
                    jour.Код_сотрудника = addedData[i].Код_сотрудника;
                    jour.Справочник_ключи = addedData[i].Справочник_ключи;
                    jour.Справочник_сотрдники = addedData[i].Справочник_сотрдники;
                    jour.Статус = false;
                }
                // Заносим данные в нашу таблицу
                db.Журнал.Add(jour);
                // Обязательно сохраняем изменения
                db.SaveChanges();
            }
            dgData.Rows.Clear();
            this.Close();
        }

        // Удаление записи из таблицы выдачи
        private void delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dgData.SelectedRows)
            {
                dgData.Rows.RemoveAt(item.Index);
            }
        }

        // Подключение к базе данных и загрузкой данных из таблицы
        private void ManagerKeys_Load(object sender, EventArgs e)
        {
            // Открываем и закрываем соединение с базой данных с использованием очисти памяти
            using (db)
            {   // Создаём объект нашего контекста
                db = new ModelKeyBTcontext();
                // Загружаем данные из таблицы в кэш
                db.Журнал.Load();
                // Привязываем данные
                cbKey.DataSource = db.Справочник_ключи.ToList();
                cbSotr.DataSource = db.Справочник_сотрдники.ToList();
            }
        }
    }
}
