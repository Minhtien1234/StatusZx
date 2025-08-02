import os
import sys
import time
import socket
import requests
import subprocess
from datetime import datetime
from colorama import init, Fore, Style

# Khởi tạo, set màu
init(autoreset=True)
PINK = Fore.LIGHTMAGENTA_EX
GREEN = Fore.GREEN
RED = Fore.RED
CYAN = Fore.CYAN
RESET = Style.RESET_ALL

# Cài đặt gói yêu cầu của Python
required_packages = ['requests', 'colorama']

def check_and_install_packages():
    for package in required_packages:
        try:
            __import__(package)  # Kiểm tra xem gói đã được cài đặt chưa
            print(f"{GREEN}[•] Đã cài gói tài nguyên cần thiết: {package}{RESET}")
        except ImportError:
            print(f"{RED}[-] Phát hiện gói {package} chưa cài đặt, tiến hành cài đặt...{RESET}")
            subprocess.check_call([sys.executable, '-m', 'pip', 'install', package])
            print(f"{GREEN}[•] Đã cài đặt gói: {package}{RESET}")
    time.sleep(2)  # Đợi 2 giây sau khi cài đặt gói

def clear():
    os.system('cls' if os.name == 'nt' else 'clear')

def print_banner():
    banner = r"""
    ███╗   ███╗████████╗████████╗ ██████╗  ██████╗ ██╗     
    ████╗ ████║╚══██╔══╝╚══██╔══╝██╔═══██╗██╔═══██╗██║     
    ██╔████╔██║   ██║      ██║   ██║   ██║██║   ██║██║     
    ██║╚██╔╝██║   ██║      ██║   ██║   ██║██║   ██║██║     
    ██║ ╚═╝ ██║   ██║      ██║   ╚██████╔╝╚██████╔╝███████╗
    ╚═╝     ╚═╝   ╚═╝      ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝
                        MTTOOL V2
    """
    print(PINK + banner + RESET)

def loading_sequence():
    steps = [
        "[•] Đang kết nối tới máy chủ...",
        "[•] Đang kiểm tra tài nguyên..."
    ]
    for step in steps:
        print(PINK + step + RESET)
        time.sleep(1.8)
    time.sleep(0.5)

def get_ip_info():
    try:
        lan_ip = socket.gethostbyname(socket.gethostname())
    except Exception as e:
        lan_ip = "Không xác định"
        print(f"{RED}[x] Lỗi khi lấy LAN IP: {e}{RESET}")
    
    try:
        pub_ip = requests.get("https://api.ipify.org", timeout=5).text
    except Exception as e:
        pub_ip = "Không xác định"
        print(f"{RED}[x] Lỗi khi lấy WAN IP: {e}{RESET}")
    
    try:
        r = requests.get(f"http://ip-api.com/json/{pub_ip}", timeout=5).json()
        isp = r.get('isp', 'Không xác định')
    except Exception as e:
        isp = "Không xác định"
        print(f"{RED}[x] Lỗi khi lấy ISP: {e}{RESET}")
    
    return lan_ip, pub_ip, isp

def show_info():
    clear()
    print_banner()
    now = datetime.now().strftime("%d/%m/%Y %H:%M:%S")
    lan, pub, isp = get_ip_info()
    
    print(f"{PINK}[i] Thời gian: {now}{RESET}")
    print(f"{PINK}[i] LAN IP: {lan}{RESET}")
    print(f"{PINK}[i] WAN IP: {pub} ({isp}){RESET}")

def safe_input(prompt=""):
    try:
        return input(prompt)
    except (KeyboardInterrupt, EOFError):
        return None

def download_and_exec(url):
    try:
        print(f"{PINK}[•] Đang tải script...{RESET}")
        r = requests.get(url, timeout=10)
        r.raise_for_status()
        clear()
        exec(r.text, globals())
        print(f"{GREEN}[v] Đã thực thi thành công!{RESET}")
    except Exception as e:
        print(f"{RED}[x] Lỗi: {e}{RESET}")

def main_menu():
    options = {
        '1': ('Cày xu traodoisub TikTok', 'https://raw.githubusercontent.com/yourusername/exam'),
        '2': ('Cày xu traodoisub Instagram', 'https://raw.githubusercontent.com/yourusername/exam'),
        '3': ('Cày xu traodoisub Facebook', 'https://raw.githubusercontent.com/yourusername/exam'),
        '4': ('Cày xu golike TikTok [Android]', 'https://raw.githubusercontent.com/yourusername/exam'),
        '5': ('Cày xu golike Instagram', 'https://raw.githubusercontent.com/yourusername/exam'),
        '6': ('Cày xu golike Instagram', 'https://raw.githubusercontent.com/yourusername/exam'),
        '7': ('Cày xu golike Instagram', 'https://raw.githubusercontent.com/yourusername/exam'),
        '8': ('Cày xu golike Instagram', 'https://raw.githubusercontent.com/yourusername/exam'),
        '9': ('Cày xu golike Instagram', 'https://raw.githubusercontent.com/yourusername/exam'),
        '0': ('Thoát', None)
    }

    while True:
        show_info()
        print(f"\n{CYAN}╔══════════════════════════════════════╗")
        print(f"{CYAN}║          LỰA CHỌN CHỨC NĂNG          ║")
        print(f"{CYAN}╠══════════════════════════════════════╣")
        for key, (desc, _) in options.items():
            print(f"{GREEN}  [{key}] {desc:<38}{RESET}")  
        print(f"{CYAN}╚══════════════════════════════════════╝{RESET}")

        choice = safe_input(f"\n{PINK}[?] Nhập lựa chọn: {RESET}")
        if choice in options:
            if choice == '0':
                print(f"{GREEN}[v] Tiến hành thoát sau 3s.{RESET}")
                time.sleep(3)
                print(f"{GREEN}[v] Đã thoát chương trình.{RESET}")
                break
            _, url = options[choice]
            if url:
                download_and_exec(url)
        else:
            print(f"{RED}[x] Lựa chọn không hợp lệ!{RESET}")
            time.sleep(1.2)

if __name__ == "__main__":
    clear()
    loading_sequence()
    check_and_install_packages() 
    while True:
        try:
            clear()
            print_banner()
            main_menu()
        except Exception as e:
            print(f"{RED}[x] Đã xảy ra lỗi: {e}{RESET}")
            time.sleep(2)  # Đợi 2 giây trước khi quay lại menu
