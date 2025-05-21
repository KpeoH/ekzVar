import pandas as pd
import sqlite3

excel_file = 'C:/Users/badbo/OneDrive/Рабочий стол/DemoEx/WinFormsApp1/WinFormsApp1/Vakansi.xlsx'

df = pd.read_excel(excel_file)

conn = sqlite3.connect('C:/Users/badbo/OneDrive/Рабочий стол/DemoEx/WinFormsApp1/WinFormsApp1/mydata.db')

df.to_sql('Vakansi', conn, if_exists='append', index=False)

conn.close()

print("Импорт завершён.")


update Vakansi set SpecializeId = (select Id from Specialize where Specialize.Name = Vakansi.SpecializeId);

