
def checkTrait(c):
    if '\n' in c:
        return False

    return (int((ord(c) - 0xAC00) % 28) != 0)


text = open('name_db', encoding='utf-8', errors='ignore')

items = text.readlines()
text.close()

merged = set(items)

writer = open('name_db', mode='w', encoding='utf-8', errors='ignore')

writer.writelines(merged)

writer.close()
