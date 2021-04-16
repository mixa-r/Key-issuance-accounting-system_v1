//Подключаем библиотеки EF 6 для работы с базой данных
//Подключаем библиотеки ZXing для взаимодействия с камерой и считывания QR кода или штрихкода
using System;
using System.Linq;
using System.Windows.Forms;
using testBD_v._1.ModelDB;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.ComponentModel;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using ZXing;


namespace testBD_v._1
{
    public partial class JournalForm : Form
    {
        // Инициализируем объект нашего контекста
        ModelKeyBTcontext db;

        // Инициализация формы
        public JournalForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Метод загрузки данных из базы данных
        /// </summary>
        void refreshForm()
        {
            using (db)
            {
                // Создаём объект нашего контекста
                db = new ModelKeyBTcontext();
                // Загружаем данные из таблицы в кэш
                db.Журнал.Load();
                dataGridView1.RowHeadersVisible = false;
                // Привязываем данные к dataGridView
                dataGridView1.DataSource = db.Журнал.Local.ToBindingList();
                cbKey.DataSource = db.Справочник_ключи.ToList();
                cbSotr.DataSource = db.Справочник_сотрдники.ToList();

                dataGridView1.Columns["Код_сотрудника"].Visible = false;
                dataGridView1.Columns["Статус"].Visible = false;
                dataGridView1.Columns["Справочник_ключи"].Visible = false;
                dataGridView1.Columns["Справочник_сотрдники"].Visible = false;
                txtID.Enabled = false;

                // Выставляем ширину колонок
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 65;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].Width = 60;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[7].Width = 200;
                dataGridView1.Columns[9].Width = 100;

                dataGridView1.Columns[10].HeaderText = "Статус";

                // Вызываем метод окрашивания ячейки в зависимости от статуса
                Colors();

                // Очищаем поля
                chbCrit.Text = null;
                textSearch.Visible = true;
                cbSotrSearch.Visible = false;
                cbStatusSeatch.Visible = false;
                cbKeySearch.Visible = false;
                textSearchQr.Text = null;
            }
        }
        
        /// <summary>
        /// Метод меняет цвет ячейки статус красный - "не возвращен", зеленый - "возвращен"
        /// </summary>        
        private void Colors()
        {
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                //Если статус  "Возвращен", то ячейка зеленая
                if (dataGridView1.Rows[j].Cells[10].Value.ToString() == "Возвращен")
                {
                    dataGridView1.Rows[j].Cells[10].Style.BackColor = System.Drawing.Color.LawnGreen;
                }
                //Если статус  "Возвращен", то ячейка красная
                else
                {
                    dataGridView1.Rows[j].Cells[10].Style.BackColor = System.Drawing.Color.Pink;
                }
            }
        }

        // Открываем спраочник сотрдники
        private void справочникСотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserForm uForm = new UserForm();
            uForm.Show();
        }

        // Добавляем одиночную запись
        private void butAdd_Click(object sender, EventArgs e)
        {
            // Проверяем, что в текстовых полях есть данные
            if ((cbSotr.SelectedValue == null) || (cbKey.SelectedValue == null))
            {
                MessageBox.Show("Заполните данные");
                return;
            }

            //  Создаём экземпляр класса Журнал
            Журнал jour = new Журнал();
            {
                jour.Дата_выдачи = Convert.ToDateTime(dateVid.Text);
                jour.Дата_возврата = Convert.ToDateTime(dateVozv.Text);
                jour.Информация = txtInfo.Text;
                jour.Код_ключа = ((Справочник_ключи)cbKey.SelectedItem).Код_ключа;
                jour.Код_сотрудника = ((Справочник_сотрдники)cbSotr.SelectedItem).Код_сотрудника;
                if (cbStatus.SelectedItem.ToString() == "Возвращен")
                    jour.Статус = true;
                else
                    jour.Статус = false;
            }

            // Заносим данные в нашу таблицу
            db.Журнал.Add(jour);
            // Обязательно сохраняем изменения
            db.SaveChanges();
            refreshForm();            
            // Обнуляем текстовые поля
            Clear();
        }

        // Загрузка данных из базы данных
        private void Form1_Load(object sender, EventArgs e)
        {
            refreshForm();
            setCurrentSearch(textSearch);
            chbCrit.SelectedIndex = -1;
        }

        // Открываем спраочник ключи
        private void справочникКлючиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyForm keyForm = new KeyForm();
            keyForm.Show();
        }

        // Открываем спраочник районы
        private void справочникРайоныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegionForm rForm = new RegionForm();
            rForm.Show();
        }

        // Открываем спраочник расположение АВ
        private void справочниикРасположениеАВToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocationForm locForm = new LocationForm();
            locForm.Show();
        }

        // очищаем поля
        private void butClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        /// <summary>
        ///Очищаем значения полей
        /// </summary>
        void Clear()
        {
            txtID.Text = "";
            txtInfo.Text = "";
            cbSotr.SelectedItem = null;
            cbStatus.SelectedItem = null;
            dateVid.Text = null;
            dateVozv.Text = null;
            cbKey.SelectedItem = null;
        }

        // Удаление записи
        private void butDelete_Click(object sender, EventArgs e)
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
                // Находим запись по этому id с помощью метода Find()
                Журнал keys = db.Журнал.Find(id);
                // Удаляем запись из базы данных
                db.Журнал.Remove(keys);
                // Сохраняем базу данных
                db.SaveChanges();
                // Обновляем базу данных и выводим в таблицу
                refreshForm();
                // Очищаем поля
                Clear();
            }
        }

        // Редактирование записи
        private void butEdit_Click(object sender, EventArgs e)
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
            Журнал jour = db.Журнал.Find(id);
            if (jour == null) return;
            jour.Дата_выдачи = Convert.ToDateTime(dateVid.Text);
            jour.Дата_возврата = Convert.ToDateTime(dateVozv.Text);
            jour.Информация = txtInfo.Text;
            jour.Код_ключа = ((Справочник_ключи)cbKey.SelectedItem).Код_ключа;
            jour.Код_сотрудника = ((Справочник_сотрдники)cbSotr.SelectedItem).Код_сотрудника;
            if (cbStatus.SelectedItem.ToString() == "Возвращен")
                jour.Статус = true;
            else
                jour.Статус = false;
            
            // Добавляем или обновляем запись
            db.Журнал.AddOrUpdate(jour);
            db.SaveChanges();
            dataGridView1.Refresh();
        }

        // При клике на строку
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверка выборки строк
            // Если строка не выбрана, то дальше ничего не происходит
            if (dataGridView1.CurrentRow == null) return;

            // Получаем выделенную строку и приводим её у типу Countries
            Журнал jour = dataGridView1.CurrentRow.DataBoundItem as Журнал;

            // Если мы щёлкаем по пустой строке, то ничего дальше не делаем
            if (jour == null) return;

            // Выводим данные 
            txtID.Text = jour.id.ToString();
            dateVid.Text = jour.Дата_выдачи.ToString();
            dateVozv.Text = jour.Дата_возврата.ToString();
            cbKey.SelectedIndex = cbKey.FindString(jour.Справочник_ключи.Адрес);
            cbSotr.SelectedIndex = cbSotr.FindString(jour.Справочник_сотрдники.ФИО);
            txtInfo.Text = jour.Информация;
            cbStatus.SelectedItem = jour.Ключ_статус;

        }

        // Выгрузить все данные
        private void allData_Click(object sender, EventArgs e)
        {
            Stopet();
            refreshForm();
        }

        // Настраиваем видимость элементов
        void setCurrentSearch(Control elem)
        {
            cbSotrSearch.Visible = false;
            cbStatusSeatch.Visible = false;
            cbKeySearch.Visible = false;
            textSearch.Visible = false;
            elem.Visible = true;
        }

        // Кнопка поиска
        private void butSearch_Click(object sender, EventArgs e)
        {
            
            using (db)
            {
                if(chbCrit.SelectedIndex == -1)
                {
                    MessageBox.Show("Не указан критерий поиска");
                    return;
                }
                
                    //В зависимости от выбранного критерия ищем по разному
                    switch (chbCrit.SelectedIndex)
                    {
                        case 0:
                            dataGridView1.DataSource = db.Журнал.Where(x => x.Справочник_ключи.Адрес.Contains(textSearch.Text)).ToList();
                            break;
                        case 1:
                            dataGridView1.DataSource = db.Журнал.Where(x => x.Справочник_сотрдники.ФИО.Contains(textSearch.Text)).ToList();
                            break;
                        case 2:
                            string value = cbStatusSeatch.SelectedItem.ToString();
                            bool key = false;

                            if (value == "Возвращен")
                                key = true;
                            dataGridView1.DataSource = db.Журнал.Where(x => x.Статус == key).ToList();
                            break;
                        case 3:
                            dataGridView1.DataSource = db.Журнал.Where(x => x.Информация.Contains(textSearch.Text)).ToList();
                            break;
                        case 4:
                            dataGridView1.DataSource = db.Журнал.Where(x => x.Справочник_ключи.Код_ключа.ToString().Contains(textSearch.Text)).ToList();
                            break;
                    }

                
                dataGridView1.Columns["Код_сотрудника"].Visible = false;
                dataGridView1.Columns["Статус"].Visible = false;
                dataGridView1.Columns["Справочник_ключи"].Visible = false;
                dataGridView1.Columns["Справочник_сотрдники"].Visible = false;
                Colors();
            }

        }

        // Критерии поиска
        private void chbCrit_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(chbCrit.SelectedIndex)
            {
                case 0:
                    setCurrentSearch(textSearch);
                    break;

                case 1:
                    setCurrentSearch(textSearch);
                    break;

                case 2:
                    setCurrentSearch(cbStatusSeatch);
                    break;

                case 3:
                    setCurrentSearch(textSearch);
                    break;
                    
                case 4:
                    setCurrentSearch(textSearch);
                    break;
            }
        }

        // Открываем окно выдачи ключей
        private void mangerKey_Click(object sender, EventArgs e)
        {
            ManagerKeys frm = new ManagerKeys();
            frm.ShowDialog();
            refreshForm();
        }

        // Меняем статус ключей
        private void statusKey_Click(object sender, EventArgs e)
        {
            // Проверяем, что выбрана запись
            if (dataGridView1.SelectedRows.Count ==0)
            {
                MessageBox.Show("Не выбрана ни одна запись!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i=0;i<dataGridView1.SelectedRows.Count;i++)
            {
                // Получаем id из текстового поля
                int id = Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[0].Value);
                // Находим запись по этому id с помощью метода Find()
                Журнал jour = db.Журнал.Find(id);
                if (jour == null) return;

                // Если статус равен
                if (jour.Статус == false)
                {
                    jour.Статус = true;
                    // Окрашиваем ячейку статус согласно методу 
                    Colors();
                }
                else
                {
                    jour.Статус = false;
                    // Окрашиваем ячейку статус согласно методу 
                    Colors();
                }

                // Добавляем или обновляем запись
                db.Журнал.AddOrUpdate(jour);
                // Сохраняем все изменения
                db.SaveChanges();
            }
            // Обновляем табилцу
            dataGridView1.Refresh();
        }

        // Выключить камеру считывателя QR кодов
        private void QRSearch_Click(object sender, EventArgs e)
        {
            Stopet();
        }

        // Строка поиска
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Журнал.Where(x => x.Код_ключа.ToString().Contains(textSearchQr.Text)).ToList();
            Colors();
        }

        // Завершение работы
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopet();
            Application.Exit();
        }

        #region QR сканирование
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice VideoCaptureDevice;
        
        // Инициализация и поиск камер и устройств для считывания QR кодов
        private void ScanBar_Load1()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
                cboCamera.Items.Add(device.Name);
            cboCamera.SelectedIndex = 0;

            VideoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            VideoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            VideoCaptureDevice.Start();
        }

        // Сканирование QR
        public void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if (result != null)
            {
                textSearchQr.Invoke(new MethodInvoker(delegate ()
                {   
                    // Передать значение в поле поиска
                    textSearchQr.Text = result.ToString();
                }));
            }
            pictureBox.Image = bitmap;
        }

        /// <summary>
        /// Остановить прием изображения и очисть форму изображения
        /// </summary>
        private void Stopet()
        {
            if (VideoCaptureDevice != null)
            {
                if (VideoCaptureDevice.IsRunning)
                    VideoCaptureDevice.Stop();
                pictureBox.Image = null;
            }
        }

        // Включить камеру и распознование QR кода
        private void QRstart_Click(object sender, EventArgs e)
        {
            ScanBar_Load1();
        }

        // Завершение работы программы
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

    }
}
