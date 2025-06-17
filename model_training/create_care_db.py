import sqlite3

conn = sqlite3.connect("care_suggestions.db")
cursor = conn.cursor()

cursor.execute("""
CREATE TABLE IF NOT EXISTS CareInfo (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    label TEXT UNIQUE,
    suggestion TEXT
)
""")

care_data = [
    ("NORMAL", "No action needed. Patient is healthy."),
    ("PNEUMONIA", "Suggest medical attention. Common treatments: antibiotics, oxygen therapy, hospitalization depending on severity.")
]

cursor.executemany("INSERT OR IGNORE INTO CareInfo (label, suggestion) VALUES (?, ?)", care_data)
conn.commit()
conn.close()
print("Care suggestion DB created.")
