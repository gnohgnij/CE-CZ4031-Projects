"""
contains code for GUI
"""

import PySimpleGUI as sg
import psycopg2

class GUI():

    def __init__(self):
        self.initialise_GUI()

    def initialise_GUI(self):

        sg.theme('LightGrey1')   #theme of GUI
        font = 'Consolas'

        # All the stuff inside the window.
        col1 = [
            [sg.Text('Host:', font=(font, 12), justification='left')],
            [sg.Text('Port:', font=(font, 12), justification='left')],
            [sg.Text('Database:', font=(font, 12), justification='left')],
            [sg.Text('Username:', font=(font, 12), justification='left')],
            [sg.Text('Password:', font=(font, 12), justification='left')]
        ]

        col2 = [
            [sg.Input(key='host')],
            [sg.Input(key='port')],
            [sg.Input(key='database')],
            [sg.Input(key='username')],
            [sg.InputText('', key='password', password_char='*')],
        ]

        layout = [  
            [sg.Text('Query Execution Plan Annotator', key='-text-', font=(font, 20))],
            [sg.Frame(layout=col1, title=''), sg.Frame(layout=col2, title='')],
            [sg.Button('Submit')]
        ]

        # Create the Window
        window = sg.Window('CX4031 Project 2 GUI', layout, element_justification='c').Finalize()
        window.Maximize()
        # Event Loop to process "events" and get the "values" of the inputs
        while True:
            event, values = window.read()
            if event == 'Submit':   # when user clicks submit button
                host = values['host']   #localhost
                port = values['port']   #5432
                database = values['database']   #whatever ur database name is
                username = values['username']   #postgres
                password = values['password']   #whatever ur password is

                conn = self.connect_to_database(host, port, database, username, password)

            if event == sg.WIN_CLOSED: # if user closes window
                break

        window.close()
    
    def connect_to_database(self, host, port, database, username, password):
        conn = psycopg2.connect(
            host = host,
            port = port,
            database = database,
            user = username,
            password = password
        )

        return conn


def main():
    app = GUI()

if __name__ == "__main__":
    main()